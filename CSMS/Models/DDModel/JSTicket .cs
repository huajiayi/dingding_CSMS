using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{

    public class JSTicket : ResultPackage
    {
        public string ticket { get; set; }
        public int expires_in { get; set; }
        public DateTime time { get; set; }

    }  
    
}