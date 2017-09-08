using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractStatementManagementSystem
{
   public class Project
    {
       public Guid ID { get; set; }
       public Guid ContractID { set; get; } //合同日期
       public DateTime DompletedDate { get; set; } //竣工日期
       public DateTime DompletedAcceptanceDate { get; set; } //竣工验收日期
       public DateTime ProjectStart { get; set; }//施工日期
    }
}
