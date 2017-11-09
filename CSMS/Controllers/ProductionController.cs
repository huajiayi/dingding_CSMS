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
    public class ProductionController : Controller
    {
        // GET: production
        public ActionResult Production()
        {
            try { 
                ViewBag.p = "";
            string s = ViewBag.Message;
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
            ObservableCollection < Productioner >pt = SqlQuery.ProductionerQuery(ID);
            ObservableCollection < ProductionerLog >  ptl= SqlQuery.ProductionerLogQuery(ID);
            ptl = Orderby.ProductionerLogPaiXu(ptl);
            ViewBag.ContractName = ct[0].ContractName;
            ViewBag.Count = ct[0].Count;
            string[] LogDates = new string[ptl.Count];
            for (int i = 0; i < ptl.Count; i++)
            {
                LogDates[i] = ptl[i].LogDate.ToString();
            }
            ViewBag.LogDatesJson = JsonTools.ObjectToJson(LogDates);
            ViewBag.ProductionerLogJson = JsonTools.ObjectToJson(ptl);
            return View(pt[0]);
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult AddProductionLog()
        {
            try {
               
              
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
            ObservableCollection<Productioner> pt = SqlQuery.ProductionerQuery(ID);
            ObservableCollection<ProductionerLog> ptl = SqlQuery.ProductionerLogQuery(ID);
           
            ViewBag.ContractName = ct[0].ContractName;
            ViewBag.Count = ct[0].Count;
            ViewBag.s1 = pt[0].NoTotalProduct;
            ViewBag.ProductionerLogJson = JsonTools.ObjectToJson(ptl);
            return View(pt[0]);
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult saveProductionLog(ProductionerLog ptl)
        {
            try {
                ViewBag.p = "";
              
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection<Productioner> pt = SqlQuery.ProductionerQuery(ID);
            ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
            ptl.ID = Guid.NewGuid();
            ptl.ContractID = ID;
            ptl.DepartmentID = pt[0].ID;
            ptl.LogDate= DateTime.Now.ToString();
            ptl.Name = Session["username"].ToString();
            GetData.ProductionerGet(ptl, pt, ow);
            return RedirectToAction("Production");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult ProductionAjaxTT()
        {
            try {
                ViewBag.p = "";
                string s = ViewBag.Message;
                Guid ID = new Guid(Session["cc"].ToString());
                string ss = Request["ID"];
                int a = Convert.ToInt16(ss);
                ObservableCollection<ProductionerLog> osl = SqlQuery.ProductionerLogQueryLz(a, ID);
                string result = JsonTools.ObjectToJson(osl);
                return Content(result);
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult Productionmodification()
        {
            try { 
              
                Guid ID = new Guid(Session["cc"].ToString());
                ObservableCollection<Productioner> op = SqlQuery.ProductionerQuery(ID);
                ViewBag.logName = Request["logName"];
                ViewBag.log = Request["log"];
                ViewBag.date = Request["date"];
                Session["ProductionID"] = Request["ID"];
                ViewBag.ID=Request["ID"];
               string aa = Request["ID"];
                ViewBag.Name = Request["Name"];
                ViewBag.logDate = Request["logDate"];
                ViewBag.s1 = op[0].NoTotalProduct+Convert.ToDouble(Request["log"]);
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult SaveProductionmodification(ProductionerLog pl)
        {

            try { 
               
                Guid ID = new Guid(Session["cc"].ToString());
                pl.Name= Session["username"].ToString();
                Guid ID2 = new Guid(Session["ProductionID"].ToString());
                ObservableCollection<ProductionerLog>opl= SqlQuery.ProductionerLogQueryByID(ID2);
                ObservableCollection<Productioner> op = SqlQuery.ProductionerQuery(ID);
                ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
                
                GetData.SaveProductionmodification(opl[0], op[0], ow[0], pl);
                return RedirectToAction("Production");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
    }

}