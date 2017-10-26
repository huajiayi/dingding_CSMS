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
        public DateTime dt =  DateTime.Parse("1970-01-01");
    }  
    
}