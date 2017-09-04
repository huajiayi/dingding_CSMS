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
            ViewBag.ProductionerLogJson = JsonTools.ObjectToJson(ptl);
            return View(pt[0]);
        }
        public ActionResult addProductionLog()
        {

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
        public ActionResult saveProductionLog(ProductionerLog ptl)
        {

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
    }
}