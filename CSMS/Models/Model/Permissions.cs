using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class Permissions
    {
       public string ID { get; set; }
       public string Name { get; set; }
       public int Production_p { get; set; }
        public int Warehouse_p { get; set; }
        public int Project_p { get; set; }
        public int Sales_p { get; set; }
        public int Invoicings_p { get; set; }
        public int Accountant_p { get; set; }
        public int Summation_p { get; set; }
        
    }
}