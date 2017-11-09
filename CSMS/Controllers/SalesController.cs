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
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult SalesAjaxTT()
        {
            try
            {
                ViewBag.p = "";
                Guid ID = new Guid(Session["cc"].ToString());
                string ss = Request["ID"];
                int a = Convert.ToInt16(ss);

                ObservableCollection<SalesLog> osl = SqlQuery.SalesLogQueryLz(a, ID);
                string result = JsonTools.ObjectToJson(osl);
                return Content(result);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult Sales()
        {

            try
            {
                ViewBag.p = "";
                Guid ID = new Guid(Session["cc"].ToString());
                ObservableCollection<Sales> os = SqlQuery.SalesQuery(ID);
                Sales sa = os[0];
                ObservableCollection<SalesLog> osl = SqlQuery.SalesLogQuery(ID);
                osl = Orderby.SalesLogPaixu(osl);
                ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
                ViewBag.ContractName = ct[0].ContractName;
                ViewBag.Contract_Amount = ct[0].Contract_Amount;
                ViewBag.ID = ct[0].ID;
                //ViewBag.LastID = osl[osl.Count-1].ID;
                string[] LogDates = new string[osl.Count];
                for (int i = 0; i < osl.Count; i++)
                {
                    LogDates[i] = osl[i].LogDate.ToString();
                }
                ViewBag.LogDatesJson = JsonTools.ObjectToJson(LogDates);
                ViewBag.SalesLogJson = JsonTools.ObjectToJson(osl);
                return View(os[0]);
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }

        }
        public ActionResult SaveSalesLog(SalesLog sl)
        {
            try
            {
                ViewBag.p = "";
                Guid ID = new Guid(Session["cc"].ToString());
                sl.ContractID = ID;
                sl.ID = Guid.NewGuid();
                sl.LogDate = DateTime.Now.ToString();
                ViewBag.Message = Session["username"];
                sl.Name = ViewBag.Message;
                ObservableCollection<Contract_Data> cd = SqlQuery.Contract_DataByIDQuery(sl.ServiceID);
                sl.Service = cd[0].Service;
                GetData.SalesGet(sl, SqlQuery.SalesQuery(ID));
                return RedirectToAction("Sales");

            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }

        }
        public ActionResult AddSalesLog()
        {
            try
            {
                ViewBag.p = "";
                
                Guid ID = new Guid(Session["cc"].ToString());
                ObservableCollection<Contract_Data> cd = SqlQuery.ContractDataQuery(ID);
                cd = Orderby.paiXu(cd);
                ObservableCollection<Sales> os = SqlQuery.SalesQuery(ID);
                ViewBag.ss1 = os[0].NoAmountCollection;
                ViewBag.Contract_DataJson = JsonTools.ObjectToJson(cd);
                return View();

            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult SalesModification()
        {
            try
            {
                ViewBag.p = "";
                Guid ID = new Guid(Session["cc"].ToString());
                ObservableCollection<Contract_Data> cd = SqlQuery.ContractDataQuery(ID);
                ViewBag.Contract_DataJson = JsonTools.ObjectToJson(cd);
                ViewBag.logName = Request["logName"];
                ViewBag.service= Request["service"];
                ViewBag.log = Request["log"];
                ViewBag.date = Request["date"];
                string s9= Request["ID"];
                ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
                ViewBag.ss1 = ct[0].Contract_Amount;
                Session["salesid"]= Request["ID"];
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult SaveSalesChangeLog(SalesLog sl)
        {
            try
            {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                }
                ViewBag.Message = Session["cc"];
                string s = ViewBag.Message;
                ViewBag.p= Session["salesid"];
                string sss = ViewBag.p;
                sl.Name= Session["username"].ToString();
                Guid ID2 = new Guid(sss);
                Guid ID = new Guid(s);
                sl.ContractID = ID;
                sl.ID = ID2;
                ObservableCollection<Contract_Data> cd = SqlQuery.Contract_DataByIDQuery(sl.ServiceID);
                sl.Service = cd[0].Service;
                GetData.SalesChangeGet(sl, SqlQuery.SalesQuery(ID));
                return RedirectToAction("Sales");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }


        }
    }
}