using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractStatementManagementSystem;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text;

namespace WebApplication4.Controllers
{
    public class SummationController : Controller
    {
        // GET: Summation
        public ActionResult Summation()
        {
            try
            {
             
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection<Productioner> opr = SqlQuery.ProductionerQuery(ID);
            ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
            ObservableCollection<Project_data> opj = SqlQuery.Project_dataQuery(ID);
            ObservableCollection<Sales> osl = SqlQuery.SalesQuery(ID);
            ObservableCollection<Accountant> oac = SqlQuery.AccountantQuery(ID);
            oac = Orderby.AccountantPaixuByService(oac);
            opj = Orderby.Project_dataPaixu(opj);
            ObservableCollection<ContractNameT> oct = SqlQuery.ContractVQuery(ID);
            ObservableCollection<Invoicing> oin = SqlQuery.Invoicing(ID);
            ObservableCollection<ProductionerLog> pl = SqlQuery.ProductionerLogQueryAll(ID);
            ObservableCollection<WarehouseLog> owl = SqlQuery.WarehouseLogQueryAll(ID);
            ObservableCollection<SalesLog> osll = SqlQuery.SalesLogQueryAll(ID);
            ViewBag.InvoicingLogJson = JsonTools.ObjectToJson(SqlQuery.InvoicingAll(ID));
            ViewBag.AccountantLogJson = JsonTools.ObjectToJson(SqlQuery.AccountantLogQueryAll(ID));
            ViewBag.ProjectLogJson = JsonTools.ObjectToJson(SqlQuery.ProjectLogQueryAll(ID));
            ViewBag.SalesLogJson = JsonTools.ObjectToJson(osll);
            ViewBag.WarehouseLogJson = JsonTools.ObjectToJson(owl);
            ViewBag.ProductionerLogJson = JsonTools.ObjectToJson(pl);
            ViewBag.ProductionerJson = JsonTools.ObjectToJson(opr);
            ViewBag.WarehouseJson = JsonTools.ObjectToJson(ow);
            ViewBag.ProjectJson = JsonTools.ObjectToJson(opj);
            ViewBag.SalesJson = JsonTools.ObjectToJson(osl);
            ViewBag.AccountantJson = JsonTools.ObjectToJson(oac);
            ViewBag.ContractNameTJson = JsonTools.ObjectToJson(oct);
            ViewBag.InvoicingJson = JsonTools.ObjectToJson(oin);
            ViewBag.Contract_Date = oct[0].Contract_Date.ToShortDateString();
            return View();
        }
             catch
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }

        }
    }
}