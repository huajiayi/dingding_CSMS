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
                Guid ID = new Guid(Session["cc"].ToString());
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
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }

        }
        public ActionResult AddWarehouseLog()
        {
            try {
                ViewBag.p = "";
                Guid ID = new Guid(Session["cc"].ToString());
                ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
                ViewBag.s1 = ow[0].NoShippedCount;
                ViewBag.Reserves = ow[0].Reserves;
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult saveWarehouseLog(WarehouseLog wl)
        {
            try {
                ViewBag.p = "";
                Guid ID = new Guid(Session["cc"].ToString());
                ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
                wl.DepartmentID = ow[0].ID;
                wl.ContractID = ID;
                wl.ID = Guid.NewGuid();
                wl.LogDate = DateTime.Now.ToString();
                ViewBag.Message = Session["username"];
                wl.Name = ViewBag.Message;
                GetData.WarehouseGet(wl, ow);
                return RedirectToAction("Warehouse"); 
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult WarehouseAjaxTT()
        {
            try {
                ViewBag.p = "";
        
                Guid ID = new Guid(Session["cc"].ToString());
                string ss = Request["ID"];
                int a = Convert.ToInt16(ss);

                ObservableCollection<WarehouseLog> osl = SqlQuery.WarehouseLogQuery(a, ID);
                string result = JsonTools.ObjectToJson(osl);
                return Content(result);
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult WarehouseLogModification() {
            try { 
          
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection < Warehouse > ob= SqlQuery.WarehouseQuery(ID);
            ViewBag.logName = Request["logName"];
            ViewBag.date = Request["date"];
            ViewBag.log = Request["log"];
            Session["WarehouseLogID"] = Request["ID"];
            ViewBag.logDate = Request["logDate"];
            ViewBag.s1 = ob[0].Reserves + Convert.ToDouble(Request["log"]);
            return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult SaveWarehouseLogModification(WarehouseLog wl)
        {
            try { 
            Guid ID = new Guid(Session["cc"].ToString());
          
            wl.Name= Session["username"].ToString();
           
            Guid ID2 = new Guid(Session["WarehouseLogID"].ToString());
            ObservableCollection < Warehouse > ow= SqlQuery.WarehouseQuery(ID);
            ObservableCollection < WarehouseLog > owl= SqlQuery.WarehouseLogQueryByID(ID2);
            GetData.SaveWarehouseLogModification(ow[0], owl[0], wl);
            return RedirectToAction("Warehouse");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
    }
}