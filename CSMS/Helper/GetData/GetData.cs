using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
namespace ContractStatementManagementSystem
{
    public class GetData
    {
        public static ObservableCollection<Warehouse> WarehouseGet(WarehouseLog w, ObservableCollection<Warehouse> ow)
        {
            SqlQuery.insert(w);
            //ObservableCollection <WarehouseLog> owl= SqlQuery.WarehouseLogQuery(w.ContractID);
            ObservableCollection<ProductionerLog> op = SqlQuery.ProductionerLogQuery(w.ContractID);
            //ObservableCollection<Warehouse> ow =  SqlQuery.WarehouseQuery(w.ContractID);
            //var a = owl.Sum(x => x.Shipments);
            var b = op.Sum(x => x.ProductionCount);
            //Warehouse wah = new Warehouse();
            //wah.ContractID = w.ContractID;
            //wah.ID = ow[0].ID;
            ow[0].ShippedCount +=w.Shipments;
            ow[0].Reserves -= w.Shipments;
            ow[0].NoShippedCount -= w.Shipments;
            SqlQuery.updata(ow[0]);
            //ObservableCollection<object> ob = new ObservableCollection<object>();
            //ob.Add(owl);
            //ob.Add();
            return ow;

        }
        public static ObservableCollection<Productioner> ProductionerGet(ProductionerLog pl, ObservableCollection<Productioner> op,ObservableCollection<Warehouse> w )
        {
            SqlQuery.insert(pl);
            //var a = opl.Sum(x => x.ProductionCount);
            op[0].TotalProduct += pl.ProductionCount;
            op[0].NoTotalProduct -=pl.ProductionCount;
            w[0].Reserves = op[0].TotalProduct - w[0].ShippedCount;
            SqlQuery.updata(op[0]);
            SqlQuery.updata(w[0]);
            return op;
        }
        public static ObservableCollection<Sales> SalesGet(SalesLog sl, ObservableCollection<Sales> os) {
            SqlQuery.insert(sl);
           // os[0].AmountCollection += sl.AmountCollection;
            os[0].NoAmountCollection -= sl.AffirmIncomeAmount;
            os[0].SubAffirmIncomeAmount += sl.AffirmIncomeAmount;
            os[0].SubInvoiceAmount += sl.InvoiceAmount;
            os[0].SubInvoiceCount += sl.InvoiceCount;
            SqlQuery.updata(os[0]);
            return os;
        }
        public static ObservableCollection<Sales> SalesChangeGet(SalesLog sl, ObservableCollection<Sales> os)
        {
            ObservableCollection<SalesLog> sl2 = SqlQuery.SalesLogQueryByID(sl.ID);
            sl.LogDate = DateTime.Now.ToString();
            SqlQuery.updata(sl);
            // os[0].AmountCollection = sl.AmountCollection-sl.AmountCollection;
            os[0].NoAmountCollection = os[0].NoAmountCollection+ sl2[0].AffirmIncomeAmount-sl.AffirmIncomeAmount;
            os[0].SubAffirmIncomeAmount = os[0].SubAffirmIncomeAmount + sl.AffirmIncomeAmount - sl2[0].AffirmIncomeAmount;
            //os[0].SubInvoiceAmount += sl.InvoiceAmount;
            //os[0].SubInvoiceCount += sl.InvoiceCount;;
            SqlQuery.updata(os[0]);
            return os;
        }
        public static void ProjectGet(ProjectLog pjl,Project_data pd) {
            SqlQuery.insert(pjl);
            //int a = opj.IndexOf(mw.ppt.FirstOrDefault<Project_data>(x => x.ID == item.ID));
            //pds = SqlQuery.Project_dataQueryByService(mw.ct.ID, item.ID);
            //opj.Add(pd);
            //Project_data a =opj.FirstOrDefault<Project_data>(x => x.ServiceID == pd.ServiceID);
            //opj[opj.IndexOf(a)] = pd;
            //int i = opj.IndexOf(a);
            //Boolean bb = (i >= 0);
            //if (bb)
            //{
            //    opj.Move(opj.IndexOf(pd), opj.IndexOf(a));
            //    opj.Remove(a);
            //    ObservableCollection < Project_data > ppp= SqlQuery.Project_dataQueryByService(pd.ContractID, pd.ServiceID);
            //    //p
            //}
            SqlQuery.insert(pd);
        }
        public static void AccountantGet(AccountantLog al, Accountant a) {
            Accountant ab = (Accountant)a.Clone();
            al.Amount = Convert.ToDecimal(al.AffirmIncomeAmount) - a.SubAffirmIncomeAmount;
            a.SubAffirmIncomeAmount = Convert.ToDecimal(al.AffirmIncomeAmount);
            a.AffirmIncomeGist = al.AffirmIncomeGist;
            a.SubInvoiceAmount = Convert.ToDecimal(al.InvoiceAmount);
            a.SubInvoiceCount = Convert.ToDouble(al.InvoiceCount);
            a.SubManufacturing_Costs = Convert.ToDecimal(al.Manufacturing_Costs);
            a.SubMaterial = Convert.ToDecimal(al.Material);
            a.Subtotal = Convert.ToDecimal(al.Subtotal);
            a.Subworker = Convert.ToDecimal(al.worker);
            a.AvgGrossrofitMargin = Convert.ToDouble(al.GrossrofitMargin);
            a.SubCost = Convert.ToDouble(al.Cost);
            a.AffirmIncomeDate = al.AffirmIncomeDate;
            SqlQuery.updata(a);
            bool flag = false;
            if (al.LogName != null) {
                flag = true;
            }
            if (ab.SubAffirmIncomeAmount != Convert.ToDecimal(al.AffirmIncomeAmount)) {
                flag = true;
                al.AffirmIncomeAmount = " 由 " + ab.SubAffirmIncomeAmount + " 更改为 " + Convert.ToDecimal(al.AffirmIncomeAmount);
            }
            if (ab.AffirmIncomeGist != al.AffirmIncomeGist)
            {
                flag = true;
                al.AffirmIncomeGist = " 由 " + ab.AffirmIncomeGist + " 更改为 " + al.AffirmIncomeGist;
            }
            if (ab.SubInvoiceAmount != Convert.ToDecimal(al.InvoiceAmount))
            {
                flag = true;
                al.InvoiceAmount = " 由 " + ab.SubInvoiceAmount + "更改为" + Convert.ToDecimal(al.InvoiceAmount);
            }
            if (ab.SubInvoiceCount != Convert.ToDouble(al.InvoiceCount))
            {
                flag = true;
                al.InvoiceCount = " 由 " + ab.SubInvoiceCount + " 更改为 " + Convert.ToDouble(al.InvoiceCount);
            }
            if (ab.SubManufacturing_Costs != Convert.ToDecimal(al.Manufacturing_Costs))
            {
                flag = true;
                al.Manufacturing_Costs = " 由 " + ab.SubManufacturing_Costs + " 更改为 " + Convert.ToDecimal(al.Manufacturing_Costs);
            }
            if (ab.SubMaterial != Convert.ToDecimal(al.Material))
            {
                flag = true;
                al.Material = " 由 " + ab.SubMaterial + " 更改为 " + Convert.ToDecimal(al.Material);
            }
            if (ab.Subtotal != Convert.ToDecimal(al.Subtotal))
            {
                flag = true;
                al.Subtotal = " 由 " + ab.Subtotal + " 更改为 " + Convert.ToDecimal(al.Subtotal);
            }
            if (ab.Subworker != Convert.ToDecimal(al.worker))
            {
                flag = true;
                al.worker = " 由 " + ab.Subworker + " 更改为 " + Convert.ToDecimal(al.worker);
            }
            if (ab.SubCost != Convert.ToDouble(al.Cost))
            {
                flag = true;
                al.Cost = " 由 " + ab.SubCost + " 更改为 " + Convert.ToDouble(al.Cost);
            }
            if (ab.AvgGrossrofitMargin != Convert.ToDouble(al.GrossrofitMargin))
            {
                flag = true;
                al.GrossrofitMargin = " 由 " + ab.AvgGrossrofitMargin + " 更改为 " + Convert.ToDouble(al.GrossrofitMargin);
            }
            string sa = "";
            if (ab.AffirmIncomeDate != null) { 
                if(ab.AffirmIncomeDate.Contains(" 0:00:00"))
              sa = ab.AffirmIncomeDate.Replace(" 0:00:00", "").Replace("/", "-");
            
            if (ab.AffirmIncomeDate.Contains(" 12:00:00 AM")) {
                sa= ab.AffirmIncomeDate.Replace(" 12:00:00 AM", "").Replace("/", "-");
            }
            }
            if (sa != al.AffirmIncomeDate) {
                al.AffirmIncomeDate = " 由 " + sa + " 更改为 " + al.AffirmIncomeDate;
                flag = true;
            }
            if(flag  == true){
                SqlQuery.insert(al);
            }
        }
        public static void first (ContractNameT ct,Contract_Data cd){
            Productioner pr = new Productioner();
            pr.ContractID = ct.ID;
            pr.NoTotalProduct = ct.Count;
            pr.TotalProduct = 0;
            pr.ID = Guid.NewGuid();
            Sales sl = new Sales();
            sl.ID = Guid.NewGuid();
            sl.ContractID = ct.ID;
            sl.AmountCollection = 0;
            sl.NoAmountCollection = ct.Contract_Amount;
            sl.SubAffirmIncomeAmount = 0;
            sl.SubInvoiceCount = 0;
            sl.SubInvoiceAmount = 0;
            Warehouse wh = new Warehouse();
            wh.ID = Guid.NewGuid();
            wh.ContractID = ct.ID;
            wh.NoShippedCount = ct.Count;
            wh.Reserves = 0;
            wh.ShippedCount = 0;
            SqlQuery.Contractinsert(ct,pr,sl,wh);
         }
        public static double[] GetYOYincrease(int fun)
        {
            decimal c;
            int Year = DateTime.Now.Year;
            double[] YOYincrease = new double[12];
            ObservableCollection<decimal> Tyear = GetAmount(Year,fun);
            ObservableCollection<decimal> Lyear = GetAmount(Year - 1,fun);
            for (int i = 0; i < Tyear.Count; i++)
            {
                if (Lyear[i] == 0)
                {
                    if (Tyear[i] == 0)
                    {
                        c = 0;
                    }
                    else {
                        c = 1.00M;
                    }
                }
                else
                {
                   
                        c = Math.Round(((Tyear[i] - Lyear[i]) / Lyear[i]), 2);
                   
                }
                
                YOYincrease[i] = Convert.ToDouble(c);
            }
            return YOYincrease;
        }
        public static double[] SIncreaseRate(int fun)
        {
            decimal c;
            int Year = DateTime.Now.Year;
            double[] YOYincrease = new double[12];
            ObservableCollection<decimal> Tyear = GetAmount(Year,fun);
            ObservableCollection<decimal> Lyear = GetAmount(Year - 1,fun);
            for (int i = 0; i < Tyear.Count; i++)
            {
                if (i == 0)
                {
                    if (Tyear[i] == 0)
                    {
                        if (Lyear[11] == 0)
                        {
                            c = 0;
                        }
                        else {
                            c = Math.Round(((Tyear[i] - Lyear[11]) / Lyear[11]), 2);
                        }

                    }
                    else
                    {
                        if (Lyear[11] == 0)
                        {
                            c = 1.00M;
                        }
                        else {
                            c = Math.Round(((Tyear[i] - Lyear[11]) / Lyear[11]), 2);
                        }

                    }
                    YOYincrease[i] = Convert.ToDouble(c);
                }
                else {
                    if (Tyear[i-1] == 0)
                    {
                        if (Tyear[i] == 0)
                        {
                            c = 0M;
                        }
                        else {
                            c = 1.00M;
                        }
                    }
                    else
                    {
                        if (Tyear[i] == 0)
                        {
                            c = -1.00M;
                        }
                        else {
                            c = Math.Round(((Tyear[i] - Tyear[i - 1]) / Tyear[i - 1]), 2);
                        }
                    }
                YOYincrease[i] = Convert.ToDouble(c);
                }
            }
            return YOYincrease;
        }
        public static ObservableCollection<decimal> GetAmount(int Year,int fun){
            ObservableCollection<decimal> TAmount = new ObservableCollection<decimal>();
            string[] start = { Year + "-01" + "-01", Year + "-02" + "-01", Year + "-03" + "-01", Year + "-04" + "-01", Year + "-05" + "-01", Year + "-06" + "-01", Year + "-07" + "-01", Year + "-08" + "-01", Year + "-09" + "-01", Year + "-10" + "-01", Year + "-11" + "-01", Year + "-12" + "-01" };
            string[] End = { Year + "-01" + "-30", Year + "-02" + "-29", Year + "-03" + "-31", Year + "-04" + "-30", Year + "-05" + "-31", Year + "-06" + "-30", Year + "-07" + "-31", Year + "-08" + "-31", Year + "-09" + "-30", Year + "-10" + "-31", Year + "-11" + "-30", Year + "-12" + "-31" };
            if (Year % 4 == 0 && Year % 100 != 0 || Year % 400 == 0)
            {
                End[1] = Year + "-02" + "-29";
            }
            else
            {
                End[1] = Year + "-02" + " -28";
            }
            for (int i = 0; i < 12; i++)
            {
                decimal Am = 0;
                  switch (fun)
                {
                    case 1:
                        Am = SqlQuery.ContractGetAmount(start[i], End[i]);
                        break;
                    case 2:
                        Am = SqlQuery.InvoiceAmountChart(start[i], End[i]);
                        break;
                    case 3:
                        Am = SqlQuery.AffirmIncomeAmountChart(start[i], End[i]);
                        break;
                    case 4:
                        Am = SqlQuery.ContractGetNoAmountCollection(start[i], End[i]);
                        break;
                }
                TAmount.Add(Am);
            }
            return TAmount;
        }
        public static double[] GetYOYincrease(string type,int fun)
        {
            decimal c;
            int Year = DateTime.Now.Year;
            double[] YOYincrease = new double[12];
            ObservableCollection<decimal> Tyear = GetAmount(Year, type,fun);
            ObservableCollection<decimal> Lyear = GetAmount(Year - 1, type,fun);
            for (int i = 0; i < Tyear.Count; i++)
            {
                if (Lyear[i] == 0)
                {
                    if (Tyear[i] == 0)
                    {
                        c = 0;
                    }
                    else {
                        c = 1.00M;
                    }
                }
                else {
                    c = Math.Round(((Tyear[i] - Lyear[i]) / Lyear[i]), 2);
                }

                YOYincrease[i] = Convert.ToDouble(c);
            }
            return YOYincrease;
        }
        public static double[] SIncreaseRate(string type,int fun)
        {
            decimal c;
            int Year = DateTime.Now.Year;
            double[] YOYincrease = new double[12];
            ObservableCollection<decimal> Tyear = GetAmount(Year, type,fun);
            ObservableCollection<decimal> Lyear = GetAmount(Year - 1, type,fun);
            for (int i = 0; i < Tyear.Count; i++)
            {
                if (i == 0)
                {
                    if (Lyear[11] == 0)
                    {
                        if (Tyear[0] == 0)
                        {
                            c = 0M;
                        }
                        else {
                            c = 1.00M;
                        } 
                    }
                    else
                    {
                            c = Math.Round(((Tyear[i] - Lyear[11]) / Lyear[11]), 2);
                    }
                    YOYincrease[i] = Convert.ToDouble(c);
                }
                else {
                    if (Tyear[i - 1] == 0)

                    {
                        if (Tyear[i] == 0)
                        {
                            c = 0M;
                        }
                        else {
                            c = 1.00M;
                        }
                    }
                    else
                    {
                        if (Tyear[i] == 0)
                        {
                            c = 0M;
                        }
                        else {
                            c = Math.Round(((Tyear[i] - Tyear[i - 1]) / Tyear[i - 1]), 2);
                        }
                    }
                    YOYincrease[i] = Convert.ToDouble(c);
                }
            }
            return YOYincrease;
        }
        public static ObservableCollection<decimal> GetAmount(int Year, string type,int fun)
        {
            
            ObservableCollection<decimal> TAmount = new ObservableCollection<decimal>();
            string[] start = { Year + "-01" + "-01", Year + "-02" + "-01", Year + "-03" + "-01", Year + "-04" + "-01", Year + "-05" + "-01", Year + "-06" + "-01", Year + "-07" + "-01", Year + "-08" + "-01", Year + "-09" + "-01", Year + "-10" + "-01", Year + "-11" + "-01", Year + "-12" + "-01" };
            string[] End = { Year + "-01" + "-30", Year + "-02" + "-29", Year + "-03" + "-31", Year + "-04" + "-30", Year + "-05" + "-31", Year + "-06" + "-30", Year + "-07" + "-31", Year + "-08" + "-31", Year + "-09" + "-30", Year + "-10" + "-31", Year + "-11" + "-30", Year + "-12" + "-31" };
            if (Year % 4 == 0 && Year % 100 != 0 || Year % 400 == 0)
            {
                End[1] = Year + "-02" + "-29";
            }
            else
            {
                End[1] = Year + "-02" + " -28";
            }
            for (int i = 0; i < 12; i++)
            {
                decimal Am=0;
                switch (fun)
                {
                    case 1:
                        Am = SqlQuery.ContractGetAmountByType(start[i], End[i], type);
                        break;
                    case 2:
                        Am = SqlQuery.InvoiceAmountChart(start[i], End[i], type);
                        break;
                    case 3:
                        Am = SqlQuery.AffirmIncomeAmountChart(start[i], End[i], type);
                        break;
                    case 4:
                        Am = SqlQuery.ContractGetNoAmountCollectionByType(start[i], End[i],type);
                        break;
                }
                   TAmount.Add(Am);
            }
            return TAmount;
        }
        public static void GetExcel()
        {

            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            string filePath = @"C:\testDir\test2.xlsx";
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Sex", typeof(string));
            DataRow dr = dt.NewRow();
            dr["ID"] = "1";
            dr["Name"] = "张三";
            dr["Sex"] = "男";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "2";
            dr["Name"] = "李四";
            dr["Sex"] = "男";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "12";
            dr["Name"] = "旺旺";
            dr["Sex"] = "男";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "22";
            dr["Name"] = "王五";
            dr["Sex"] = "男";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["ID"] = "32";
            dr["Name"] = "赵六";
            dr["Sex"] = "男";
            dt.Rows.Add(dr);
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    // 2007版本
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook();
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    using (fs = File.OpenWrite(@filePath))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据  

                    }
                }
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                throw ex;
            }
        }
        public static void InvoicingChange(Guid ID, Invoicing inv,Guid ID2) {
            ObservableCollection<Accountant> oa = SqlQuery.AccountantQuery(ID);
            ObservableCollection<Invoicing> oin = SqlQuery.InvoicingByID(ID2);
            Accountant aa = null;
            Accountant aa2 = null;
            if (inv.Service == oin[0].Service)
            {
                foreach (Accountant a in oa)
                {
                    if (a.Service == inv.Service)
                    {
                        aa = a;
                        aa.SubInvoiceAmount += inv.Amount - oin[0].Amount;
                        aa.SubInvoiceCount += inv.Count - oin[0].Count;
                    }

                }
                SqlQuery.updataAcc(aa);
            }
            else {
                foreach (Accountant a in oa)
                {
                    if (a.Service == inv.Service)
                    {
                        aa = a;
                        aa.SubInvoiceAmount += inv.Amount ;
                        aa.SubInvoiceCount += inv.Count ;
                    }
                    if (a.Service==oin[0].Service) {
                        aa2 = a;
                        aa2.SubInvoiceAmount += -oin[0].Amount;
                        aa2.SubInvoiceCount += -oin[0].Count;
                    }

                }
                SqlQuery.updataAcc(aa2);
                SqlQuery.updataAcc(aa);
            }
               
              
                inv.ID = ID2;
                inv.Contract_ID = ID;
                inv.LogDate=DateTime.Now.ToString();


          
            SqlQuery.updata(inv);

           
        }
        public static void SaveProductionmodification(ProductionerLog pl, Productioner p, Warehouse w, ProductionerLog pl2) {
            p.TotalProduct += pl2.ProductionCount - pl.ProductionCount;
            p.NoTotalProduct -= pl2.ProductionCount - pl.ProductionCount;
            w.Reserves = p.TotalProduct - w.ShippedCount;
            pl2.ID = pl.ID;
            pl2.ContractID = pl.ContractID;
            pl2.LogDate = DateTime.Now.ToString();
            SqlQuery.updata(pl2);
            SqlQuery.updata(p);
            SqlQuery.updata(w);
        }
        public static void SaveWarehouseLogModification(Warehouse w, WarehouseLog wl, WarehouseLog wl2)
        {
            w.Reserves += wl.Shipments - wl2.Shipments;
            w.ShippedCount += wl2.Shipments - wl.Shipments;
            w.NoShippedCount += wl.Shipments - wl2.Shipments;
            wl2.LogDate = DateTime.Now.ToString();
            wl2.ID = wl.ID;
            wl2.ContractID = wl.ContractID;
            wl2.LogDate = DateTime.Now.ToString();
            SqlQuery.updata(wl2);
            SqlQuery.updata(w);
        }
    }
}
