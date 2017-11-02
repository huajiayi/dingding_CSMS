using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class AccessToken
    {
        public String Value { get; set; }

        /// <summary>  
        /// 票据的开始时间  
        /// </summary>  
        public DateTime Begin { get; set; } = DateTime.Parse("1970-01-01");

       
    }
}