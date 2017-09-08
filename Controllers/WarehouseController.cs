using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractStatementManagementSystem;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Web.Script.Serialization;


namespace WebApplication4.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        public ActionResult Warehouse()
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
                ObservableCollection<WarehouseLog> owl = SqlQuery.WarehouseLogQuery(ID);
                ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
                owl = Orderby.WarehouseLogPaixu(owl);
                ViewBag.ContractName = ct[0].ContractName;
                ViewBag.Count = ct[0].Count;
                string[] LogDates = new string[owl.Count];
                for (int i = 0; i < owl.Count; i++)
                {
                    LogDates[i] = owl[i].LogDate.ToString();
                }
                ViewBag.LogDatesJson = JsonTools.ObjectToJson(LogDates);
                ViewBag.WarehouseLogJson = JsonTools.ObjectToJson(owl);
                return View(ow[0]); }
            catch (Exception)
            {
                ViewBag.p = "操作异常已退回首页请刷新重试";
                return RedirectToAction("Login", "ContractandSales");
            }

        }
        public ActionResult addWarehouseLog()
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
                ViewBag.s1 = ow[0].NoShippedCount;
                ViewBag.Reserves = ow[0].Reserves;
                return View();
            }
            catch (Exception)
            {
                ViewBag.p = "操作异常已退回首页请刷新重试";
                return RedirectToAction("Login", "ContractandSales");
            }
        }
        public ActionResult saveWarehouseLog(WarehouseLog wl)
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
                wl.DepartmentID = ow[0].ID;
                wl.ContractID = ID;
                wl.ID = Guid.NewGuid();
                wl.LogDate = DateTime.Now.ToString();
                GetData.WarehouseGet(wl, ow);
                return RedirectToAction("Warehouse"); }
            catch (Exception)
            {
                ViewBag.p = "操作异常已退回首页请刷新重试";
                return RedirectToAction("Login", "ContractandSales");
            }
        }
        public ActionResult WarehouseAjaxTT()
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                string ss = Request["ID"];
                int a = Convert.ToInt16(ss);

                ObservableCollection<WarehouseLog> osl = SqlQuery.WarehouseLogQuery(a, ID);
                string result = JsonTools.ObjectToJson(osl);
                return Content(result);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "ContractandSales", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
    }
}