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
            catch (Exception)
            {
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
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
    }

}