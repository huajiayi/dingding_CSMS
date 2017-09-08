using Dapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractStatementManagementSystem
{
    public class SqlQuery
    {
        public static string @string = ConfigurationManager.ConnectionStrings["MHCC_sales"].ConnectionString;
        public static void Contractinsert( ContractNameT ct,Productioner pr,Sales sa,Warehouse wh) {
            using (var conn = new SqlConnection(@string))
            {
                string sql0 = String.Format(@"insert into ContractNameT (ID,Customer,Contract_Type,Contract_Amount,Count,Contract_Number,Contract_Date,ContractName) values(N'{7}',N'{0}',N'{1}',{2},{3},N'{4}',N'{5}',N'{6}');", ct.Customer, ct.Contract_Type, ct.Contract_Amount, ct.Count, ct.Contract_Number, ct.Contract_Date,ct.ContractName,ct.ID);
                string sql3 = String.Format(@"insert into Productioner (ID,ContractID,TotalProduct,NoTotalProduct) values(N'{0}',N'{1}',{2},{3});",pr.ID,pr.ContractID,pr.TotalProduct,pr.NoTotalProduct);
                string sql5 = String.Format(@"insert into Sales (ID,ContractID,AmountCollection,NoAmountCollection,SubAffirmIncomeAmount,SubInvoiceCount,SubInvoiceAmount) values(N'{0}',N'{1}',{2},N'{3}',{4},{5},{6});", sa.ID,sa.ContractID,sa.AmountCollection,sa.NoAmountCollection,sa.SubAffirmIncomeAmount,sa.SubInvoiceCount,sa.SubInvoiceAmount);
                string sql6 = String.Format(@"insert into Warehouse (ID,ContractID,Reserves,ShippedCount,NoShippedCount) values(N'{0}',N'{1}',{2},{3},{4});",wh.ID,wh.ContractID,wh.Reserves,wh.ShippedCount,wh.NoShippedCount);
                string[] sqls = { sql0,sql3, sql5,sql6 };
                string s=string.Concat(sqls);
                conn.Open();
                SqlCommand cmd = new SqlCommand(s, conn);
                cmd.ExecuteNonQuery();

            }
        }
        public static ObservableCollection<ContractNameT> ContractQuery() {

            string sql = @"select * from(select row_number() over(order by [Contract_Date] desc) as rownum, * FROM [ContractNameT]  ) as r where r.rownum >0 and rownum <=20";
            ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
            return ww;
        }
        public static ObservableCollection<ContractNameT> ContractQuery(int a)
        {

            string sql = String.Format(@"select * from(select row_number() over(order by [Contract_Date] desc) as rownum, * FROM [ContractNameT]  ) as r where r.rownum >{0} and rownum <={1}",a,a+20);
            ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
            return ww;
        }
        public static ObservableCollection<ContractNameT> ContractVQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [ContractNameT] where ID='{0}'",id);
            ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
            return ww;
        }
        public static ObservableCollection<ContractNameT> ContractVQueryByName(string name,int N)
        {
            string sql = String.Format(@"select * from(select row_number() over(order by G.[Contract_Date] desc) as rownum, * FROM (SELECT * FROM [ContractNameT] where ContractName  LIKE  N'%{0}%')as G) as r where r.rownum >{1} and rownum <={2}", name,N,N+20);
            ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
            return ww;
        }
        //public static ObservableCollection<ContractNameT> ContractVQueryBystart(string start)
        //{
        //    string sql = String.Format(@"select * from(select row_number() over(order by G.[Contract_Date] desc) as rownum, * FROM (SELECT * FROM [ContractNameT] where Contract_Date> '{0}')as G) as r where r.rownum >{1} and rownum <={2}", start,);
        //    ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
        //    return ww;
        //}
        //public static ObservableCollection<ContractNameT> ContractVQueryByend(string end,int N)
        //{
        //    string sql = String.Format(@"select * from(select row_number() over(order by G.[Contract_Date] desc) as rownum, * FROM (SELECT * FROM [ContractNameT] where Contract_Date<'{0}')as G) as r where r.rownum >{1} and rownum <={2}", end,N,N+30);
        //    ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
        //    return ww;
        //}
        public static ObservableCollection<ContractNameT> ContractVQueryByDate(string strat, string end,int N) 
        {
            string sql = String.Format(@"select * from(select row_number() over(order by G.[Contract_Date] desc) as rownum, * FROM (SELECT * FROM [ContractNameT] where Contract_Date>='{0}' and Contract_Date<='{1}')as G) as r where r.rownum >{2} and rownum <={3}", strat, end,N,N+20);
            ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
            return ww;
        }
        public static ObservableCollection<ContractNameT> ContractVQueryByDateandName(string name,string start, string end,int N) 
        {
            string sql = String.Format(@"select * from(select row_number() over(order by G.[Contract_Date] desc) as rownum, * FROM (select * from(SELECT * FROM [ContractNameT] where Contract_Date>='{2}' and Contract_Date<='{1}')as D where D.ContractName LIKE N'%{0}%')as G) as r where r.rownum >{3} and rownum <={4}", name, start, end,N,N+20);
            ObservableCollection<ContractNameT> ww = new ObservableCollection<ContractNameT>(Query<ContractNameT>(sql));
            return ww;
        }
        public static ObservableCollection<Productioner> ProductionerQuery(Guid id){
             string sql = String.Format(@"SELECT * FROM [Productioner] where ContractID='{0}'", id);
             ObservableCollection<Productioner> ww = new ObservableCollection<Productioner>(Query<Productioner>(sql));
             return ww;
            }
        public static ObservableCollection<ProductionerLog> ProductionerLogQuery(Guid id)
        {
             string sql = String.Format(@"select * from(select row_number() over(order by [LogDate] desc) as rownum, * FROM [ProductionerLog] where ContractID='{0}' ) as r where r.rownum >0 and rownum <=5", id);
        ObservableCollection<ProductionerLog> ww = new ObservableCollection<ProductionerLog>(Query<ProductionerLog>(sql));
            return ww;
        }
        public static ObservableCollection<ProductionerLog> ProductionerLogQueryLz(int a,Guid id)
        {
            string sql = String.Format(@"select * from( select row_number() over(order by [LogDate] desc) as rownum, * FROM [ProductionerLog] where ContractID='{2}' ) as r where r.rownum >{0} and rownum <={1}", a, a + 5, id);
            ObservableCollection<ProductionerLog> ww = new ObservableCollection<ProductionerLog>(Query<ProductionerLog>(sql));
            return ww;
        }
        public static ObservableCollection<Warehouse> WarehouseQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [Warehouse] where ContractID='{0}'", id);
            ObservableCollection<Warehouse> ww = new ObservableCollection<Warehouse>(Query<Warehouse>(sql));
            return ww;
        }
        public static ObservableCollection<WarehouseLog> WarehouseLogQuery (Guid id)
        {
            string sql = String.Format(@"select * from(select row_number() over(order by [LogDate] desc) as rownum, * FROM [WarehouseLog] where ContractID='{0}' ) as r where r.rownum >0 and rownum <=5", id);
            ObservableCollection<WarehouseLog> ww = new ObservableCollection<WarehouseLog>(Query<WarehouseLog>(sql));
            return ww;
        }
        public static ObservableCollection<WarehouseLog> WarehouseLogQuery(int a, Guid id)
        {
            string sql = String.Format(@"select * from(select row_number() over(order by [LogDate] desc) as rownum, * FROM [WarehouseLog] where ContractID='{2}' ) as r where r.rownum >{0}and rownum <={1}", a, a + 5, id);
            ObservableCollection<WarehouseLog> ww = new ObservableCollection<WarehouseLog>(Query<WarehouseLog>(sql));
            return ww;
        }
        public static ObservableCollection<Sales> SalesQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [Sales] where ContractID='{0}'", id);
            ObservableCollection<Sales> ww = new ObservableCollection<Sales>(Query<Sales>(sql));
            return ww;
        }
        public static ObservableCollection<SalesLog> SalesLogQuery(Guid id)

        {
            string sql = String.Format(@"select * from(select row_number() over(order by [LogDate] desc) as rownum, * FROM [SalesLog] where ContractID='{0}' ) as r where r.rownum >0 and rownum <=5", id);
            ObservableCollection<SalesLog> ww = new ObservableCollection<SalesLog>(Query<SalesLog>(sql));
            return ww;
        }
        public static ObservableCollection<SalesLog> SalesLogQueryLz(int a,Guid ID)

        {
            string sql = String.Format(@"select * from( select row_number() over(order by [LogDate] desc) as rownum, * FROM [SalesLog] where ContractID='{2}' ) as r where r.rownum >{0} and rownum <={1}", a,a+5,ID);
            ObservableCollection<SalesLog> ww = new ObservableCollection<SalesLog>(Query<SalesLog>(sql));
            return ww;
        }
        public static ObservableCollection<Contract_Data> ContractDataQuery(Guid id)

        {
            string sql = String.Format(@"SELECT * FROM [Contract_Data] where Contract_ID='{0}'", id);
            ObservableCollection<Contract_Data> ww = new ObservableCollection<Contract_Data>(Query<Contract_Data>(sql));
            return ww;
        }
        public static ObservableCollection<Contract_Data> Contract_DataByIDQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [Contract_Data] where ID='{0}'", id);
            ObservableCollection<Contract_Data> ww = new ObservableCollection<Contract_Data>(Query<Contract_Data>(sql));
            return ww;
        }
        public static ObservableCollection<Project> ProjectQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [Project] where ContractID='{0}'", id);
            ObservableCollection<Project> ww = new ObservableCollection<Project>(Query<Project>(sql));
            return ww;
        }
        //public static ObservableCollection<ProjectLog> ProjectLogQuery(Guid id)
        //{
        //    string sql = String.Format(@"select * from(select row_number() over(order by [LogDate] desc) as rownum, * FROM [ProjectLog] where ContractID='{0}' ) as r where r.rownum >0 and rownum <=5", id);
        //    ObservableCollection<ProjectLog> ww = new ObservableCollection<ProjectLog>(Query<ProjectLog>(sql));
        //    return ww;
        //}
             public static ObservableCollection<ProjectLog> ProjectLogQuery( int a,Guid id)
        {
            string sql = String.Format(@"select * from( select row_number() over(order by [LogDate] desc) as rownum, * FROM [ProjectLog] where ContractID='{2}' ) as r where r.rownum >{0} and rownum <={1}", a, a + 5, id);
            ObservableCollection<ProjectLog> ww = new ObservableCollection<ProjectLog>(Query<ProjectLog>(sql));
            return ww;
        }
    
        public static ObservableCollection<ProjectLog> ProjectLogByIDQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [ProjectLog] where ID='{0}'", id);
            ObservableCollection<ProjectLog> ww = new ObservableCollection<ProjectLog>(Query<ProjectLog>(sql));
            return ww;
        }
        public static ObservableCollection<Project_data> Project_dataQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [Project_data] where ContractID='{0}'", id);
            ObservableCollection < Project_data > ww = new ObservableCollection< Project_data > (Query < Project_data > (sql));
            return ww;
        }
        public static ObservableCollection<Project_data> Project_dataQueryByService(Guid sid)
        {
            string sql = String.Format(@"SELECT * FROM [Project_data] where  ServiceID='{0}'",sid);
            ObservableCollection<Project_data> ww = new ObservableCollection<Project_data>(Query<Project_data>(sql));
            return ww;
        }
        public static ObservableCollection<Accountant> AccountantQuery(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [Accountant] where ContractID='{0}'", id);
            
            ObservableCollection<Accountant> ww =new ObservableCollection<Accountant>(Query<Accountant>(sql));
            return ww;
        }
        public static ObservableCollection<Accountant> AccountantByServiceQuery(Guid id) 
        {
            string sql = String.Format(@"SELECT * FROM [Accountant] where ServiceID='{0}'", id);

            ObservableCollection<Accountant> ww = new ObservableCollection<Accountant>(Query<Accountant>(sql));
            return ww;
        }
        public static ObservableCollection<AccountantLog> AccountantLogQuery(Guid id)
        {
            string sql = String.Format(@"select * from(select row_number() over(order by [LogDate] desc) as rownum, * FROM [Accountantlog] where ContractID='{0}' ) as r where r.rownum >0 and rownum <=5", id);
            ObservableCollection<AccountantLog> ww = new ObservableCollection<AccountantLog>(Query<AccountantLog>(sql));
            return ww;
        }
        public static ObservableCollection<AccountantLog> AccountantLogQuery(int a, Guid id)
        {
            string sql = String.Format(@"select * from( select row_number() over(order by [LogDate] desc) as rownum, * FROM [Accountantlog] where ContractID='{2}' ) as r where r.rownum >{0} and rownum <={1}", a, a + 5, id);
            ObservableCollection<AccountantLog> ww = new ObservableCollection<AccountantLog>(Query<AccountantLog>(sql));
            return ww;
        }
        public static ObservableCollection<AccountantLog> AccountantLogQueryByID(Guid id)
        {
            string sql = String.Format(@"SELECT * FROM [Accountantlog] where ID='{0}'", id);
            ObservableCollection<AccountantLog> ww = new ObservableCollection<AccountantLog>(Query<AccountantLog>(sql));
            return ww;
        }
        public static void insert( object ob)
        {
            using (var conn = new SqlConnection(@string))
            {
                if (ob is ProjectLog) {
                    ProjectLog a = (ProjectLog)ob;
                    string sql0 = @"insert into ProjectLog(ID,DepartmentID,DompletedDate,DompletedAcceptanceDate,LogDate,Name,ContractID,LogName,ServiceID,ProjectStart,Service) values(@ID,@DepartmentID,@DompletedDate,@DompletedAcceptanceDate,@LogDate,@Name,@ContractID,@LogName,@ServiceID,@ProjectStart,@Service)";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@DepartmentID", a.DepartmentID);
                    dic.Add("@DompletedDate", a.DompletedDate);
                    dic.Add("@DompletedAcceptanceDate", a.DompletedAcceptanceDate);
                    dic.Add("@LogDate", a.LogDate);
                    dic.Add("@Name", a.Name);
                    dic.Add("@ContractID", a.ContractID);
                    dic.Add("@LogName", a.LogName);
                    dic.Add("@ServiceID", a.ServiceID);
                    dic.Add("@ProjectStart", a.ProjectStart);
                    dic.Add("@Service", a.Service);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if (ob is Project_data)
                {
                    Project_data a = (Project_data)ob;
                    string sql0 = @"UPDATE Project_data SET DompletedDate=@DompletedDate,DompletedAcceptanceDate=@DompletedAcceptanceDate,ServiceID=@ServiceID,Service=@Service,ProjectStart=@ProjectStart,ContractID=@ContractID WHERE ServiceID = @ServiceID;IF(@@ROWCOUNT = 0) BEGIN INSERT INTO Project_data(ID, DompletedDate,DompletedAcceptanceDate,ServiceID,Service,ProjectStart,ContractID)VALUES(@ID,@DompletedDate,@DompletedAcceptanceDate,@ServiceID,@Service,@ProjectStart,@ContractID)END;";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@DompletedDate", a.DompletedDate);
                    dic.Add("@DompletedAcceptanceDate", a.DompletedAcceptanceDate);
                    dic.Add("@ServiceID", a.ServiceID);
                    dic.Add("@Service", a.Service);
                    dic.Add("@ProjectStart", a.ProjectStart);
                    dic.Add("@ContractID", a.ContractID);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if (ob is AccountantLog)
                {
                    AccountantLog a = (AccountantLog)ob;
                    string sql0 = @"insert into AccountantLog(ID,DepartmentID,AffirmIncomeGist,AffirmIncomeAmount,InvoiceCount,InvoiceAmount,Cost,Material,worker,Manufacturing_Costs,Subtotal,GrossrofitMargin,ContractID,LogDate,LogName,ServiceID,Name,service) values(@ID,@DepartmentID,@AffirmIncomeGist,@AffirmIncomeAmount,@InvoiceCount,@InvoiceAmount,@Cost,@Material,@worker,@Manufacturing_Costs,@Subtotal,@GrossrofitMargin,@ContractID,@LogDate,@LogName,@ServiceID,@Name,@service)";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID",a.ID);
                    dic.Add("@DepartmentID", a.DepartmentID);
                    dic.Add("@AffirmIncomeGist", a.AffirmIncomeGist);
                    dic.Add("@AffirmIncomeAmount", a.AffirmIncomeAmount);
                    dic.Add("@InvoiceCount", a.InvoiceCount);
                    dic.Add("@InvoiceAmount", a.InvoiceAmount);
                    dic.Add("@Cost", a.Cost);
                    dic.Add("@Material", a.Material);
                    dic.Add("@worker", a.worker);
                    dic.Add("@Manufacturing_Costs", a.Manufacturing_Costs);
                    dic.Add("@Subtotal", a.Subtotal);
                    dic.Add("@GrossrofitMargin", a.GrossrofitMargin);
                    dic.Add("@ContractID", a.ContractID);
                    dic.Add("@LogDate", a.LogDate);
                    dic.Add("@LogName", a.LogName);
                    dic.Add("@ServiceID", a.ServiceID);
                    dic.Add("@Name", a.Name);
                    dic.Add("@Service", a.Service);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if (ob is ProductionerLog)
                {
                    ProductionerLog a = (ProductionerLog)ob;
                    string sql0 = @"insert into ProductionerLog(ID,DepartmentID,ProductionCount,ProductionDate,LogDate,ContractID,LogName,Name) values(@ID,@DepartmentID,@ProductionCount,@ProductionDate,@LogDate,@ContractID,@LogName,@Name)";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@DepartmentID", a.DepartmentID);
                    dic.Add("@ProductionCount", a.ProductionCount);
                    dic.Add("@ProductionDate", a.ProductionDate);
                    dic.Add("@LogDate", a.LogDate);
                    dic.Add("@ContractID", a.ContractID);
                    dic.Add("@LogName", a.LogName);
                    dic.Add("@Name", a.Name);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if (ob is WarehouseLog)
                {
                    WarehouseLog a = (WarehouseLog)ob;
                    string sql0 = @"insert into WarehouseLog(ID,DepartmentID,Shipments,ShippedDate,LogDate,ContractID,LogName,Name) values(@ID,@DepartmentID,@Shipments,@ShippedDate,@LogDate,@ContractID,@LogName,@Name)";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@DepartmentID", a.DepartmentID);
                    dic.Add("@Shipments", a.Shipments);
                    dic.Add("@ShippedDate", a.ShippedDate);
                    dic.Add("@LogDate", a.LogDate);
                    dic.Add("@ContractID", a.ContractID);
                    dic.Add("@LogName", a.LogName);
                    dic.Add("@Name", a.Name);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if (ob is SalesLog)
                {
                    SalesLog a = (SalesLog)ob;
                    string sql0 = @"insert into SalesLog(ID,DepartmentID,ReturnDate,InvoiceDate,AffirmIncomeDate,AffirmIncomeAmount,InvoiceCount,InvoiceAmount,AmountCollection,AffirmIncomeGist,ContractID,LogDate,LogName,ServiceID,Name,Service) values(@ID,@DepartmentID,@ReturnDate,@InvoiceDate,@AffirmIncomeDate,@AffirmIncomeAmount,@InvoiceCount,@InvoiceAmount,@AmountCollection,@AffirmIncomeGist,@ContractID,@LogDate,@LogName,@ServiceID,@Name,@Service)";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@DepartmentID", a.DepartmentID);
                    dic.Add("@ReturnDate", a.ReturnDate);
                    dic.Add("@InvoiceDate", a.InvoiceDate);
                    dic.Add("@AffirmIncomeDate", a.AffirmIncomeDate);
                    dic.Add("@AffirmIncomeAmount", a.AffirmIncomeAmount);
                    dic.Add("@InvoiceCount", a.InvoiceCount);
                    dic.Add("@InvoiceAmount", a.InvoiceAmount);
                    dic.Add("@AmountCollection", a.AmountCollection);
                    dic.Add("@AffirmIncomeGist", a.AffirmIncomeGist);
                    dic.Add("@ContractID", a.ContractID);
                    dic.Add("@LogDate", a.LogDate);
                    dic.Add("@LogName", a.LogName);
                    dic.Add("@ServiceID", a.ServiceID);
                    dic.Add("@Name", a.Name);
                    dic.Add("@Service", a.Service);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if (ob is Contract_Data)
                {
                    Contract_Data a = (Contract_Data)ob;
                    string sql0 = @"insert into Contract_Data(ID,Service,Contract_ID) values(@ID,@Service,@Contract_ID)";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@Contract_ID", a.Contract_ID);
                    dic.Add("@Service", a.Service);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if  (ob is Accountant)
                    {
                    Accountant a = (Accountant)ob;
                    string sql0 = @"insert into Accountant (ID,ContractID,AffirmIncomeGist,SubAffirmIncomeAmount,SubInvoiceCount,SubInvoiceAmount,SubCost,SubMaterial,Subworker,SubManufacturing_Costs,AvgGrossrofitMargin,NoAffirmIncomeAmount,Subtotal,ServiceID,Service) values(@ID,@ContractID,@AffirmIncomeGist,@SubAffirmIncomeAmount,@SubInvoiceCount,@SubInvoiceAmount,@SubCost,@SubMaterial,@Subworker,@SubManufacturing_Costs,@AvgGrossrofitMargin,@NoAffirmIncomeAmount,@Subtotal,@ServiceID,@Service);";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@AffirmIncomeGist",a.AffirmIncomeGist);
                    dic.Add("@SubAffirmIncomeAmount", a.SubAffirmIncomeAmount);
                    dic.Add("@SubInvoiceCount", a.SubInvoiceCount);
                    dic.Add("@SubInvoiceAmount", a.SubInvoiceAmount);
                    dic.Add("@SubCost", a.SubCost);
                    dic.Add("@SubMaterial", a.SubMaterial);
                    dic.Add("@Subworker", a.Subworker);
                    dic.Add("@SubManufacturing_Costs", a.SubManufacturing_Costs);
                    dic.Add("@AvgGrossrofitMargin", a.AvgGrossrofitMargin);
                    dic.Add("@ContractID", a.ContractID);
                    dic.Add("@NoAffirmIncomeAmount", a.NoAffirmIncomeAmount);
                    dic.Add("@Subtotal", a.Subtotal);
                    dic.Add("@ServiceID", a.ServiceID);
                    dic.Add("@Service", a.Service);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);
                }
              

            }

        }
        public static void updata( object ob) {
            using (var conn = new SqlConnection(@string))
            {

                if (ob is Accountant)
                {
                    Accountant a = (Accountant)ob;
                    string sql0 = @"update Accountant set ID=@ID,ContractID=@ContractID,AffirmIncomeGist=@AffirmIncomeGist, SubAffirmIncomeAmount=@SubAffirmIncomeAmount, SubInvoiceCount=@SubInvoiceCount,SubInvoiceAmount=@SubInvoiceAmount,SubCost=@SubCost,SubMaterial=@SubMaterial,Subworker=@Subworker,SubManufacturing_Costs=@SubManufacturing_Costs,Subtotal=@Subtotal,AvgGrossrofitMargin=@AvgGrossrofitMargin,NoAffirmIncomeAmount=@NoAffirmIncomeAmount where ID=@ID";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@AffirmIncomeGist", a.AffirmIncomeGist);
                    dic.Add("@SubAffirmIncomeAmount", a.SubAffirmIncomeAmount);
                    dic.Add("@SubInvoiceCount", a.SubInvoiceCount);
                    dic.Add("@SubInvoiceAmount", a.SubInvoiceAmount);
                    dic.Add("@SubCost", a.SubCost);
                    dic.Add("@SubMaterial", a.SubMaterial);
                    dic.Add("@Subworker", a.Subworker);
                    dic.Add("@SubManufacturing_Costs", a.SubManufacturing_Costs);
                    dic.Add("@AvgGrossrofitMargin", a.AvgGrossrofitMargin);
                    dic.Add("@ContractID", a.ContractID);
                    dic.Add("@NoAffirmIncomeAmount", a.NoAffirmIncomeAmount);
                    dic.Add("@Subtotal", a.Subtotal);
                    string s = string.Concat(sql0);
                    conn.Open();
                    conn.Execute(s, dic);

                }

                if (ob is Productioner)
                {
                    Productioner a = (Productioner)ob;
                    string s = @"update Productioner set ID=@ID,ContractID=@ContractID,TotalProduct=@TotalProduct, NoTotalProduct=@NoTotalProduct where ID=@ID";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@TotalProduct", a.TotalProduct);
                    dic.Add("@NoTotalProduct", a.NoTotalProduct);   
                    dic.Add("@ContractID", a.ContractID);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                if (ob is Sales) {
                    Sales a = (Sales)ob;
                    string s = @"update Sales set ID=ID,ContractID=@ContractID,AmountCollection=@AmountCollection, NoAmountCollection=@NoAmountCollection, SubAffirmIncomeAmount=@SubAffirmIncomeAmount,SubInvoiceCount=@SubInvoiceCount,SubInvoiceAmount=@SubInvoiceAmount  where ID=@ID";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@AmountCollection", a.AmountCollection);
                    dic.Add("@NoAmountCollection", a.NoAmountCollection);
                    dic.Add("@SubAffirmIncomeAmount", a.SubAffirmIncomeAmount);
                    dic.Add("@SubInvoiceCount", a.SubInvoiceCount);
                    dic.Add("@SubInvoiceAmount", a.SubInvoiceAmount);
                    dic.Add("@ContractID", a.ContractID);
                    conn.Open();
                    conn.Execute(s, dic);
                }
                    if (ob is Warehouse)
                    {
                    Warehouse a = (Warehouse)ob;
                    string s = @"update Warehouse set ID=@ID,ContractID=@ContractID,Reserves=@Reserves,ShippedCount=@ShippedCount, NoShippedCount=@NoShippedCount where ID=@ID";
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@Reserves", a.Reserves);
                    dic.Add("@NoShippedCount", a.NoShippedCount);
                    dic.Add("@ShippedCount", a.ShippedCount);
                    dic.Add("@ContractID", a.ContractID);
                    conn.Open();
                    conn.Execute(s, dic);

                }
                if (ob is Project)
                {
                    Project a = (Project)ob;
                    string s = @"update Project set ID=@ID,ContractID=@ContractID,DompletedDate=@DompletedDate,DompletedAcceptanceDate=@DompletedAcceptanceDate,ProjectStart=@ProjectStart where ID=@ID";
                    conn.Open();
                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@DompletedDate", a.DompletedDate);
                    dic.Add("@DompletedAcceptanceDate", a.DompletedAcceptanceDate);
                    dic.Add("@ProjectStart", a.ProjectStart);
                    dic.Add("@ContractID", a.ContractID);

                    conn.Open();
                    conn.Execute(s, dic);

                }
                if (ob is Contract_Data)
                {
                    Contract_Data a = (Contract_Data)ob;
                    string s = String.Format(@"update Contract_Data set ID=@ID,Contract_ID=Contract_ID,Service=Service where ID=ID", a.ID,a.Contract_ID,a.Service);

                    var dic = new Dictionary<string, object>();
                    dic.Add("@ID", a.ID);
                    dic.Add("@Service", a.Service);

                    conn.Open();
                    conn.Execute(s, dic);

                }

            }
            }

        public static void DeleteContract(Guid a)
        {

            string s = String.Format(@"delete from ContractNameT where ID ='{0}'", a);
            using (var conn = new SqlConnection(@string))
            {
                gongong(conn, s);
            }

        }
        public static void DeleteService(Guid a)
        {
            string s1 = string.Format(@"delete from AccountantLog where ServiceID ='{0}';", a);
            string s2 = string.Format(@"delete from Project_data where ServiceID ='{0}';", a);
            string s = String.Format(@"delete from Contract_Data where ID ='{0}';", a);
            string ss = string.Concat(s1,s2,s);
            using (var conn = new SqlConnection(@string))
            {
                gongong(conn, ss);
            }
        }
        public static void UpdataService(Contract_Data a)
        {

            string s = String.Format(@"update Contract_Data set Service='{0}'where ID='{1}'", a.Service, a.Contract_ID);

            using (var conn = new SqlConnection(@string))
            {
                gongong(conn, s);
            }
        }
        public static void UpdataD(Contract_Data a)
        {

            string s0 = String.Format(@"update AccountantLog set Service='{0}'where ServiceID='{1}';", a.Service, a.ID);
            string s1 = String.Format(@"update ProjectLog set Service='{0}'where ServiceID='{1}';", a.Service, a.ID);
            string s2 = String.Format(@"update Project_data set Service='{0}'where ServiceID='{1}';", a.Service, a.ID);
            string s4 = String.Format(@"update SalesLog set Service='{0}'where ServiceID='{1}';", a.Service, a.ID);
            StringBuilder sb = new StringBuilder();
            sb.Append(s0);
            sb.Append(s1);
            sb.Append(s2);
            sb.Append(s4);
            //string s = string.Concat(s0,s1,s2,s4);
            using (var conn = new SqlConnection(@string))
            {
                gongong(conn, sb.ToString());
            }
        }
        public static IEnumerable<T> Query<T>(string sql, object parameter = null) 
        {
            try
            {
                using (var conn = new SqlConnection(@string))
                {
                    conn.Open();

                    return conn.Query<T>(sql, parameter);
                }
            }
              catch(Exception e) {
                throw e;
            }

        }
        public static void gongong(SqlConnection conn, string s)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(s, conn);
            cmd.ExecuteNonQuery();

        }
    }
}
