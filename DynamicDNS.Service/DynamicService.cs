using DNSPod.Api;
using DNSPod.Api.Content;
using DynamicDNS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Xml;

namespace DynamicDNS.Service {
    public partial class DynamicService : ServiceBase {

        #region 属性
        private static DNSPodClient client = null;
        private int updateInterval = 5;
        private Timer timer;
        private string email;
        private string password;
        private string domain;
        private string subDomain;
        private string token;
        private string tokenID;
        private bool isLock = false;
        private string recordType;

        #endregion


        public DynamicService() {
            InitializeComponent();

            try {
                var doc = new XmlDocument();
                doc.Load(string.Format("{0}DynamicDNS.Settings.exe.config", AppDomain.CurrentDomain.BaseDirectory));
                var appSettings = doc.GetElementsByTagName("add").OfType<XmlNode>().ToDictionary(t => t.Attributes["key"].Value.ToLower(), (t) => t.Attributes["value"].Value);

                if (appSettings.ContainsKey("email")) {
                    email = appSettings["email"];

                    if (!string.IsNullOrWhiteSpace(email)) {
                        email = CryptHelper.AESDecrypt(email);
                    }
                }

                if (appSettings.ContainsKey("password")) {
                    password = appSettings["password"];

                    if (!string.IsNullOrWhiteSpace(password)) {
                        password = CryptHelper.AESDecrypt(password);
                    }
                }

                if (appSettings.ContainsKey("domain")) {
                    domain = CryptHelper.AESDecrypt(appSettings["domain"]);
                }

                if (appSettings.ContainsKey("subdomain")) {
                    subDomain = CryptHelper.AESDecrypt(appSettings["subdomain"]);
                }
                
                if (appSettings.ContainsKey("token")) {
                    token = appSettings["token"];

                    if (!string.IsNullOrWhiteSpace(token)) {
                        token = CryptHelper.AESDecrypt(token);
                    }
                }

                if (appSettings.ContainsKey("tokenid")) {
                    tokenID = appSettings["tokenid"];
                }

                if (appSettings.ContainsKey("recordtype"))
                {
                    recordType = appSettings["recordtype"];
                    Logger.Write("记录类型：" + recordType);
                }

                if (appSettings.ContainsKey("updateinterval")) {
                    var _updateInterval = appSettings["updateinterval"];
                    int.TryParse(_updateInterval, out updateInterval);
                    updateInterval = Math.Max(updateInterval, 5);
                }

            }
            catch (Exception ex) {
                Logger.Write("配置文件不存在或配置不正确：{0}", ex.Message);
            }

            timer = new Timer();
            timer.Elapsed += timer_Elapsed;
        }

        protected override void OnStart(string[] args) {

            Logger.Write("服务启动！" + DateTime.Now);

            if((string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(tokenID)) &&
               (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))) {
                Logger.Write("至少需要一种验证方式！");
                this.Stop();
                return;
            }

            if (string.IsNullOrWhiteSpace(domain)) {
                Logger.Write("Missing Domain");
                this.Stop();
                return;
            }

            if (string.IsNullOrWhiteSpace(subDomain)) {
                Logger.Write("Missing SubDomain");
                this.Stop();
                return;
            }

            if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(tokenID)) {
                Logger.Write("===>>>" + tokenID);
                client = new DNSPodClient(tokenID +"," + token);
            }
            else {
                client = new DNSPodClient(email, password);
            }

            timer.Interval = updateInterval * 60 * 1000;
            timer.Start();

            AppHelper.SetTimeout(() => {
                DDNS(client, domain, subDomain, recordType);
            }, 1000);
        }

        protected override void OnStop() {
        }

        protected void timer_Elapsed(object sender, ElapsedEventArgs e) {
            DDNS(client, domain, subDomain, recordType);
        }

        private bool DDNS(DNSPodClient client, string domainName, string subDomain, string recordType) {
            if (!isLock) {
                isLock = true;
                Logger.Write("开始获取本地IP,记录类型：" + recordType);
                var ip = string.Empty;
                if (recordType == "A")
                {
                    ip = DNSHelper.GetLocalIP();
                    Logger.Write("本地IP为：{0}，IP比对中...", ip);
                }
                else if (recordType == "AAAA")
                {
                    ip = DNSHelper.GetLocalIPv6();
                    Logger.Write("本地IP为：{0}，IP比对中...", ip);
                } else
                {
                    throw new Exception("记录类型有误：" + recordType + "。仅支持A记录和AAAA记录!");
                }

                try {
                    Logger.Write("Start GetDomain===>" + domainName);
                    Domain domain = client.GetDomain(domainName);
                    Record record = null;

                    try {
                        client.Clear();
                        Logger.Write("Start GetRecord===>" + subDomain);
                        record = client.GetRecord(domain.Id.ToString(), subDomain, recordType);
                    }
                    catch (DNSPodException ex) {
                        Logger.Write("Start CreateRecord===>" + domain.Id.ToString());
                        // 如果记录不存在则创建一个
                        if (ex.Code == 10 || ex.Code == 22) {
                            Logger.Write("主机头不存在，创建记录，类型：" + recordType + "," + ip);
                            record = client.CreateRecord(domain.Id.ToString(), subDomain, ip, recordType);
                            client.Clear();
                            Logger.Write("已创建记录，ID为：{0}，值为：{1}，类型为：{2}", record.Id, ip, recordType);
                            isLock = false;
                            return true;
                        }
                        else
                            throw ex;
                    }
                    Logger.Write("打印记录值：{0}", record.Value);
                    // 如果本地IP与服务器不一样则更新
                    if (ip == string.Empty)
                    {
                        Logger.Write("空IP，网络有问题，请检查");
                    }
                    else if (ip != record.Value) {
                        Logger.Write("IP变动，刷新DNS。IP地址为：{0}，记录类型为：{1}", ip, recordType);
                        if (recordType == "A")
                        {
                            client.DDNS(domain.Id.ToString(), record.Id, subDomain, ip);
                            client.Clear();
                            Logger.Write("已更换IP：{0}", ip);
                        }
                        else if (recordType == "AAAA")
                        {
                            client.ModifyRecord(domain.Id.ToString(), record.Id, subDomain, ip, recordType);
                            client.Clear();
                            Logger.Write("已更换IPv6：{0}", ip);
                        } 
                        else
                        {
                            Logger.Write("不支持的记录类型：" + recordType);
                        }
                    }
                    else {
                        Logger.Write("本地IP与服务器IP一致，无需更新");
                    }

                    isLock = false;
                    return true;
                }
                catch (DNSPodException ex) {
                    Logger.Write("出错：{0}", ex.Message);
                    isLock = false;
                    return false;
                }
            }

            return true;
        }
    }
}
