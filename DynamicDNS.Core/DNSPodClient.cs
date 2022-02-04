using DNSPod.Api;
using DNSPod.Api.Content;
using DNSPod.Api.Request;
using DynamicDNS.Api.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDNS.Core {
    public class DNSPodClient : IDNSClient {

        private string email;
        private string password;
        private string token;
        private static ICacheClient httpCacheClient = HttpcacheClient.GetInstance("DynamicDNS");

        /// <summary>
        /// 使用token验证
        /// </summary>
        public DNSPodClient(string token) {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("token");

            this.token = token;
        }

        /// <summary>
        /// 使用帐号密码验证
        /// </summary>
        public DNSPodClient(string email, string password) {

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            this.email = email;
            this.password = password;
        }

        /// <summary>
        /// 本接口 Domain.List.Filter 用于获取域名列表
        /// </summary>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public Domain GetDomain(string domainName) {

            if (string.IsNullOrWhiteSpace(domainName))
                throw new ArgumentNullException("domainName");

            return httpCacheClient.GetCacheData(() => {
                Logger.Write("Call API:DomainsRequest");
                DomainsRequest request = new DomainsRequest();
                request.Email = email;
                request.Password = password;
                request.Token = token;
                request.type = "mine";
                request.keyword = domainName;
                var response = request.Execute();

                if (response.Domains == null)
                    throw new DNSPodException(9, "没有任何域名");

                if (response.Domains.Count(t => domainName.Equals(t.Name, StringComparison.OrdinalIgnoreCase)) == 0)
                    throw new DNSPodException(6, "域名不存在");

                Logger.Write("API Complete:DomainsRequest");
                return response.Domains.Single(t => domainName.Equals(t.Name, StringComparison.OrdinalIgnoreCase));
            }, string.Format("domain_{0}", domainName), TimeSpan.FromDays(1));
        }

        /// <summary>
        /// 本接口 Record.List 用于获取解析记录列表
        /// </summary>
        /// <param name="domainId"></param>
        /// <param name="subDomain"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public Record GetRecord(string domainId, string subDomain, string recordType) {

            if (string.IsNullOrWhiteSpace(domainId))
                throw new ArgumentNullException("domainId");

            if (string.IsNullOrWhiteSpace(subDomain))
                throw new ArgumentNullException("subDomain");

            return httpCacheClient.GetCacheData(() => {
                Logger.Write("Call API:RecordsRequest");
                RecordsRequest request = new RecordsRequest();
                request.Email = email;
                request.Password = password;
                request.Token = token;
                request.DomainId = domainId;
                request.subDomain = subDomain;
                request.RecordType = recordType;
                var response = request.Execute();

                if (response.Records == null)
                    throw new DNSPodException(10, "没有任何记录");

                if (response.Records.Count(t => subDomain.Equals(t.Name, StringComparison.OrdinalIgnoreCase)) == 0)
                    throw new DNSPodException(22, "主机头不存在");

                if (response.Records.Count(t => subDomain.Equals(t.Name, StringComparison.OrdinalIgnoreCase) && recordType.Equals(t.Type, StringComparison.OrdinalIgnoreCase)) > 1)
                    throw new DNSPodException(-10022, "主机头对应记录过多");

                Logger.Write("API Complete:RecordsRequest");

                if (!response.Records.Any(t => subDomain.Equals(t.Name, StringComparison.OrdinalIgnoreCase) && recordType.Equals(t.Type, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new DNSPodException(22,"不存在" + recordType + "类型的记录值,捕获异常重建!");
                }

                return response.Records.Single(t => subDomain.Equals(t.Name, StringComparison.OrdinalIgnoreCase) && recordType.Equals(t.Type, StringComparison.OrdinalIgnoreCase));
            }, string.Format("record_{0}_{1}", domainId, subDomain), TimeSpan.FromDays(1));

        }

        /// <summary>
        /// 本接口 Record.Create 用于添加解析记录
        /// </summary>
        /// <param name="domainId"></param>
        /// <param name="subDomain"></param>
        /// <param name="value"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public Record CreateRecord(string domainId, string subDomain, string value, string recordType) {

            if (string.IsNullOrWhiteSpace(domainId))
                throw new ArgumentNullException("domainId");

            if (string.IsNullOrWhiteSpace(subDomain))
                throw new ArgumentNullException("subDomain");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            if (string.IsNullOrWhiteSpace(recordType))
            {
                throw new ArgumentNullException("recordType");
            }

            return httpCacheClient.GetCacheData(() => {
                Logger.Write("Call API:RecordCreateRequest");
                RecordCreateRequest request = new RecordCreateRequest();
                request.Email = email;
                request.Password = password;
                request.DomainId = domainId;
                request.Token = token;
                request.SubDomain = subDomain;
                request.Value = value;
                request.RecordLine = "默认";
                request.RecordType = recordType;// "AAAA";
                var response = request.Execute();

                Logger.Write("API Complete:RecordCreateRequest");
                return response.Record;
            }, string.Format("record_{0}_{1}", domainId, subDomain), TimeSpan.FromDays(1));
        }

        /// <summary>
        /// 本接口 Record.Modify 用于修改解析记录
        /// </summary>
        /// <param name="domainId"></param>
        /// <param name="recordId"></param>
        /// <param name="subDomain"></param>
        /// <param name="value"></param>
        /// <param name="recordType"></param>
        /// <returns></returns>
        public Record ModifyRecord(string domainId, string recordId, string subDomain, string value, string recordType)
        {
            if (string.IsNullOrWhiteSpace(domainId))
                throw new ArgumentNullException("domainId");

            if (string.IsNullOrWhiteSpace(subDomain))
                throw new ArgumentNullException("subDomain");

            if (string.IsNullOrWhiteSpace(recordId))
                throw new ArgumentNullException("recordId");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            if (string.IsNullOrWhiteSpace(recordType))
                throw new ArgumentNullException("recordType");

            return httpCacheClient.GetCacheData(() => {
                Logger.Write("Call API:ModifyRecordRequest");
                RecordModifyRequest request = new RecordModifyRequest();
                request.Email = email;
                request.Password = password;
                request.Token = token;
                request.DomainId = domainId;
                request.SubDomain = subDomain;
                request.RecordId = recordId;
                request.Value = value;// DNSHelper.GetLocalIP();
                request.RecordLine = "默认";
                request.RecordType = recordType;
                request.MX = "1";
                var response = request.Execute();
                Logger.Write("API Complete:ModifyRecordRequest");
                return response.Record;
            }, string.Format("record_{0}_{1}", domainId, recordId), TimeSpan.FromDays(1));
        }

        /// <summary>
        /// 本接口 Record.Ddns 用于更新动态 DNS 记录【不支持ipv6啊】
        /// </summary>
        /// <param name="domainId"></param>
        /// <param name="recordId"></param>
        /// <param name="subDomain"></param>
        /// <param name="ip"></param>
        public void DDNS(string domainId, string recordId, string subDomain, string ip) {
            if (string.IsNullOrWhiteSpace(domainId))
                throw new ArgumentNullException("domainId");

            if (string.IsNullOrWhiteSpace(subDomain))
                throw new ArgumentNullException("subDomain");

            if (string.IsNullOrWhiteSpace(recordId))
                throw new ArgumentNullException("recordId");

            Logger.Write("Call API:DDNSRequest");
            DDNSRequest request = new DDNSRequest();
            request.Email = email;
            request.Password = password;
            request.Token = token;
            request.DomainId = domainId;
            request.SubDomain = subDomain;
            request.RecordId = recordId;
            request.Value = ip;// DNSHelper.GetLocalIP();
            request.RecordLine = "默认";
            var response = request.Execute();
            Logger.Write("API Complete:DDNSRequest");
        }

        public void Clear() {
            httpCacheClient.Clear(string.Empty);
        }
    }
}
