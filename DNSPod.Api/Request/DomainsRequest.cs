using DNSPod.Api.Core;
using DNSPod.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNSPod.Api.Request {

    /// <summary>
    /// 域名列表获取请求
    /// </summary>
    public class DomainsRequest : RequestBase<DomainsResponse> {
        /// <summary>
        /// 域名类型。
        /// </summary>
        [Parameter("type")]
        public string type { get; set; }

        /// <summary>
        /// 搜索的关键字,如果指定则只返回符合该关键字的域名。
        /// </summary>
        [Parameter("keyword")]
        public string keyword { get; set; }

        protected override string Url {
            get { return "https://dnsapi.cn/Domain.List.Filter"; }
        }
    }
}
