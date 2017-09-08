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
    public class AccountantController : Controller
    {
        // GET: Accountant
        public ActionResult Accountant()
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                ObservableCollection<Accountant> oat = SqlQuery.AccountantQuery(ID);
                ObservableCollection<AccountantLog> oal = SqlQuery.AccountantLogQuery(ID);
                ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
                ObservableCollection<Sales> cd = SqlQuery.SalesQuery(ID);
                oat = Orderby.AccountantPaixuByService(oat);
                oal = Orderby.AccountantLogPaixu(oal);
                ViewBag.ContractName = ct[0].ContractName;
                ViewBag.Contract_Amount = ct[0].Contract_Amount;
                ViewBag.SubAffirmIncomeAmount = cd[0].SubAffirmIncomeAmount;
                ViewBag.NoAmountCollection = cd[0].NoAmountCollection;
                ViewBag.Count = ct[0].Count;
                string[] LogDates = new string[oal.Count];
                for (int i = 0; i < oal.Count; i++)
                {
                    LogDates[i] = oal[i].LogDate.ToString();
                }
                ViewBag.LogDatesJson = JsonTools.ObjectToJson(LogDates);
                ViewBag.AccountantLogJson = JsonTools.ObjectToJson(oal);
                ViewBag.AccountantJson = JsonTools.ObjectToJson(oat);
                return View(); }
           catch(Exception) {
                return RedirectToAction("Login", "ContractandSales", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult AccountantLog()
        {
            try
            {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                string ss = Request["ID"];
                Guid ID2 = new Guid(ss);
                ObservableCollection<Sales> sl = SqlQuery.SalesQuery(ID);
                ObservableCollection<AccountantLog> oal = SqlQuery.AccountantLogQueryByID(ID2);
                ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
                ViewBag.ContractName = ct[0].ContractName;
                ViewBag.Contract_Amount = ct[0].Contract_Amount;
                ViewBag.SubAffirmIncomeAmount = sl[0].SubAffirmIncomeAmount;
                ViewBag.NoAmountCollection = sl[0].NoAmountCollection;
                return View(oal[0]);
            }
            catch (Exception)
            {

                return RedirectToAction("Login", "ContractandSales", new { ex = "操作异常已退回首页请刷新重试" });
            }

        }
        public ActionResult addAccountantLog()
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                    
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                ObservableCollection<Contract_Data> cd = SqlQuery.ContractDataQuery(ID);
                cd = Orderby.paiXu(cd);
                ViewBag.Contract_DataJson = JsonTools.ObjectToJson(cd);
                return View();
            }
               catch(Exception) {
                return RedirectToAction("Login", "ContractandSales", new { ex = "操作异常已退回首页请刷新重试" });
            }
            
            
          
          
        }
        public ActionResult saveAccountantLog(AccountantLog al)
        {
            try {
                ViewBag.p = "";
                if (Session["cc"] != null)
                {
                    ViewBag.Message = Session["cc"];
                }
                string s = ViewBag.Message;
                Guid ID = new Guid(s);
                al.ContractID = ID;
                al.ID = Guid.NewGuid();
                al.LogDate = DateTime.Now.ToString();
                ObservableCollection<Contract_Data> cd = SqlQuery.Contract_DataByIDQuery(al.ServiceID);
                ObservableCollection<Accountant> oac = SqlQuery.AccountantByServiceQuery(al.ServiceID);
                al.Service = cd[0].Service;
                al.Subtotal = al.worker + al.Material;
                GetData.AccountantGet(al, oac);
                return RedirectToAction("Accountant");
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "ContractandSales", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult AccountantLogAjaxTT()
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
            ObservableCollection<AccountantLog> osl = SqlQuery.AccountantLogQuery(a, ID);
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