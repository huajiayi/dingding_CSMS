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
            if (Session["cc"] != null)
            {
                ViewBag.Message = Session["cc"];
            }
            string s = ViewBag.Message;
            Guid ID = new Guid(s);
            ObservableCollection < Warehouse > ow= SqlQuery.WarehouseQuery(ID);
            ObservableCollection < WarehouseLog > owl = SqlQuery.WarehouseLogQuery(ID);
            ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
            owl = Orderby.WarehouseLogPaixu(owl);
            ViewBag.ContractName = ct[0].ContractName;
            ViewBag.Count = ct[0].Count;
            ViewBag.WarehouseLogJson = JsonTools.ObjectToJson(owl);
            return View(ow[0]);
        }
        public ActionResult addWarehouseLog()
        {
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
        public ActionResult saveWarehouseLog(WarehouseLog wl)
        {
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
            return RedirectToAction("Warehouse");
        }
    }
}