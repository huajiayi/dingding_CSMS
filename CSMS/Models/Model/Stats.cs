using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractStatementManagementSystem
{
    public class Stats
    {
        public string ContractName { get; set; }
        public decimal NoAmountCollection { get; set; }
        public decimal SubAffirmIncomeAmount { get; set; }
        public double TotalProduct { get; set; }
        public double NoTotalProduct { get; set; }
        public double ShippedCount { get; set; }
        public double NoShippedCount { get; set; }
    }
}