using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractStatementManagementSystem
{
    public class Invoicing
    {
       public Guid ID { get; set; }
       public  Guid ServiceID { get; set; }
        public string Service { get; set; }
        public string LogName { get; set; }
        public string InvoicingDate { get; set; }
        public string LogDate { get; set; }
        public double Count { get; set; }
        public Guid Contract_ID { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
    }
}