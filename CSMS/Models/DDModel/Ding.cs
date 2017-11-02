using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractStatementManagementSystem
{
    public class Ding
    {
        public int ID { get; set; } 
        public string Token { get; set; }
        public DateTime TokenTime { get; set; }
        public string Ticket { get; set; }
        public DateTime TicketTime { get; set; }
    }
}