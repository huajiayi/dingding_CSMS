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
          
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
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
        public ActionResult AddProductionLog()
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
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
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult saveProductionLog(ProductionerLog ptl)
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
            ObservableCollection<Productioner> pt = SqlQuery.ProductionerQuery(ID);
            ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
            ptl.ID = Guid.NewGuid();
            ptl.ContractID = ID;
            ptl.DepartmentID = pt[0].ID;
            
            ptl.LogDate= DateTime.Now.ToString();
            ViewBag.Message = Session["username"];
            ptl.Name = ViewBag.Message;
            GetData.ProductionerGet(ptl, pt, ow);
            return RedirectToAction("Production");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult ProductionAjaxTT()
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

                ObservableCollection<ProductionerLog> osl = SqlQuery.ProductionerLogQueryLz(a, ID);
                string result = JsonTools.ObjectToJson(osl);
                return Content(result);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult Productionmodification()
        {
            try
            {
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                ObservableCollection<Productioner> op = SqlQuery.ProductionerQuery(ID);
                ViewBag.logName = Request["logName"];
                ViewBag.log = Request["log"];
                ViewBag.date = Request["date"];
                Session["ProductionID"] = Request["ID"];
                ViewBag.Name = Request["Name"];
                ViewBag.logDate = Request["logDate"];
                ViewBag.s1 = op[0].NoTotalProduct+Convert.ToDouble(Request["log"]);
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult SaveProductionmodification(ProductionerLog pl)
        {
          
                
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                ViewBag.Message = Session["ProductionID"];
                s = ViewBag.Message;
                Guid ID2 = new Guid(s);

                ObservableCollection<ProductionerLog>opl= SqlQuery.ProductionerLogQueryByID(ID2);
                ObservableCollection<Productioner> op = SqlQuery.ProductionerQuery(ID);
                ObservableCollection<Warehouse> ow = SqlQuery.WarehouseQuery(ID);
                
                GetData.SaveProductionmodification(opl[0], op[0], ow[0], pl);
                return RedirectToAction("Production");
           
        }
    }

}