using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractStatementManagementSystem
{
   public class AccountantLog : INotifyPropertyChanged
    {
      public Guid ID { get; set; }
      public Guid DepartmentID { get; set; }
      public string AffirmIncomeGist { get; set; } //确认收入依据
      public string AffirmIncomeAmount { get; set; } //确认收入金额
      public string InvoiceCount { get; set; } //已开票数
      public string InvoiceAmount { get; set; } //已开票金额
      public string Cost { get; set; } //已结算成本数量
      public string Material { get; set; } //直接材料
      public string worker { get; set; } //工人成本
      public string Manufacturing_Costs { get; set; } //制造费用
      public string Subtotal { get; set; } //小计
      public string GrossrofitMargin { get; set; } //毛利
      public Guid ContractID { get; set; } //合同id
      public string LogDate { get; set; } //日志日期
      public string LogName { get; set; } //日志名
      public Guid ServiceID { get; set; } //服务id
      public string Name { get; set; }//操作人
      public string AffirmIncomeDate { get; set; }
 
        public string service;// 服务项目
        public string Service // 服务项目
        {
            get { return service; }

            set
            {
                service = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Service"));

                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public decimal Amount { get; set; }
    }
}
