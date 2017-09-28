using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class SignPackage
    {
        public String agentId { get; set; }

        public String corpId { get; set; }

        public String timeStamp { get; set; }

        public String nonceStr { get; set; }

        public String signature { get; set; }

        public String url { get; set; }

        public String rawstring { get; set; }

        public string jsticket { get; set; }
    }
}