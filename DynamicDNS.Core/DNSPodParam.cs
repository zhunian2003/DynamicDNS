using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicDNS.Core
{
    public class DNSPodParam
    {
        public string email { get; set; }
        public string password { get; set; }
        public string domain { get; set; }
        public string subDomain { get; set; }
        public string token { get; set; }
        public string tokenID { get; set; }
        public string recordType { get; set; }
    }
}
