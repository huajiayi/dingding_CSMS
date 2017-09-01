using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractStatementManagementSystem
{
    public class Orderby
    {
        public static ObservableCollection<Contract_Data> paiXu (ObservableCollection<Contract_Data> z)
        {
            ObservableCollection<Contract_Data> ct = z;

            var query = from ttt in ct
                        orderby ttt.Service 
                        select ttt;
            ObservableCollection < Contract_Data > s = new ObservableCollection<Contract_Data>(query);
            return s;
            
        }
        public static ObservableCollection<ProductionerLog> ProductionerLogPaiXu(ObservableCollection<ProductionerLog> z)
        {
            ObservableCollection<ProductionerLog> ct = z;

            var query = from ttt in ct
                        orderby ttt.LogDate descending
                        select ttt;
            ObservableCollection<ProductionerLog> s = new ObservableCollection<ProductionerLog> (query);
            return s;

        }

        
             public static ObservableCollection<SalesLog> SalesLogPaixu(ObservableCollection<SalesLog> z)
        {
            ObservableCollection<SalesLog> ct = z;

            var query = from ttt in ct
                        orderby ttt.LogDate descending
                        select ttt;
            ObservableCollection<SalesLog> s = new ObservableCollection<SalesLog>(query);
            return s;

        }
        public static ObservableCollection<WarehouseLog> WarehouseLogPaixu(ObservableCollection<WarehouseLog> z)
        {
            ObservableCollection<WarehouseLog> ct = z;

            var query = from ttt in ct
                        orderby ttt.LogDate descending
                        select ttt;
            ObservableCollection<WarehouseLog> s = new ObservableCollection<WarehouseLog>(query);
            return s;

        }
      
        public static ObservableCollection<Project_data> Project_dataPaixu(ObservableCollection<Project_data> z)
        {
            ObservableCollection<Project_data> ct = z;

            var query = from ttt in ct
                        orderby ttt.Service 
                        select ttt;
            ObservableCollection<Project_data> s = new ObservableCollection<Project_data>(query);
            return s;

        }
        // ObservableCollection<ProjectLog> pl = SqlQuery.ProjectLogQuery(ID);
        public static ObservableCollection<ProjectLog> ProjectLogPaixu(ObservableCollection<ProjectLog> z)
        {
            ObservableCollection<ProjectLog> ct = z;

            var query = from ttt in ct
                        orderby ttt.LogDate descending
                        select ttt;
            ObservableCollection<ProjectLog> s = new ObservableCollection<ProjectLog>(query);
            return s;

        }
       
        public static ObservableCollection<AccountantLog> AccountantLogPaixu(ObservableCollection<AccountantLog> z)
        {
            ObservableCollection<AccountantLog> ct = z;

            var query = from ttt in ct
                        orderby ttt.LogDate descending
                        select ttt;
            ObservableCollection<AccountantLog> s = new ObservableCollection<AccountantLog>(query);
            return s;

        }
        public static ObservableCollection<Accountant> AccountantPaixuByService(ObservableCollection<Accountant> z)
        {
            ObservableCollection<Accountant> ct = z;

            var query = from ttt in ct
                        orderby ttt.Service
                        select ttt;
            ObservableCollection<Accountant> s = new ObservableCollection<Accountant>(query);
            return s;

        }
    }
}
