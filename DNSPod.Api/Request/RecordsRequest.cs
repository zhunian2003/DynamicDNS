using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNSPod.Api.Core;
using DNSPod.Api.Response;

namespace DNSPod.Api.Request {

    /// <summary>
    /// 记录列表获取请求
    /// </summary>
    public class RecordsRequest : RequestBase<RecordsResponse> {

        /// <summary>
        /// 域名ID, 必选
        /// </summary>
        [Parameter("domain_id")]
        public string DomainId { get; set; }

        /// <summary>
        /// 记录类型，通过API记录类型获得，大写英文，比如：A
        /// </summary>
        [Parameter("record_type")]
        public string RecordType { get; set; }

        /// <summary>
        /// 子域名，如果指定则只返回此子域名的记录
        /// </summary>
        [Parameter("sub_domain")]
        public string subDomain { get; set; }

        protected override string Url {
            get { return "https://dnsapi.cn/Record.List"; }
        }
    }
}
