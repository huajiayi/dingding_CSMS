﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            os[0].AmountCollection += sl.AmountCollection;
            os[0].NoAmountCollection -= sl.AffirmIncomeAmount;
            os[0].SubAffirmIncomeAmount += sl.AffirmIncomeAmount;
            os[0].SubInvoiceAmount += sl.InvoiceAmount;
            os[0].SubInvoiceCount += sl.InvoiceCount;
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
        public static ObservableCollection<Accountant> AccountantGet(AccountantLog al, ObservableCollection<Accountant> a ) {
            SqlQuery.insert(al);
            
            a[0].SubAffirmIncomeAmount = al.AffirmIncomeAmount;
            a[0].AffirmIncomeGist = al.AffirmIncomeGist;
            a[0].SubInvoiceAmount = al.InvoiceAmount;
            a[0].SubInvoiceCount = al.InvoiceCount;
            a[0].SubManufacturing_Costs = al.Manufacturing_Costs;
            a[0].SubMaterial = al.Material;
            a[0].Subtotal = al.Subtotal;
            a[0].Subworker = al.worker;
            a[0].AvgGrossrofitMargin = al.GrossrofitMargin;
            a[0].NoAffirmIncomeAmount = al.AffirmIncomeAmount;
            a[0].SubCost = al.Cost;
            SqlQuery.updata(a[0]);
            return a;
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
               
    }
}
