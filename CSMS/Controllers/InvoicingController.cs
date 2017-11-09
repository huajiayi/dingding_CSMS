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
    public class InvoicingController : Controller
    {
        // GET: Invoicing AddInvoicingLog
        public ActionResult Invoicing()
        {
            try {
                ViewBag.p = "";
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection<string> name=SqlQuery.ContractVQueryName(ID);
            ObservableCollection < Accountant > oa = SqlQuery.AccountantQuery(ID);
            ObservableCollection < Invoicing > oin= SqlQuery.Invoicing(ID);
            ObservableCollection<ContractNameT> os = SqlQuery.ContractVQuery(ID);
            ViewBag.ss1 = os[0].Contract_Amount;
            oa =Orderby.AccountantPaixuByService(oa);
            ViewBag.AccountantJson= JsonTools.ObjectToJson(oa);
            ViewBag.InvoicingJson = JsonTools.ObjectToJson(oin);
            ViewBag.Name = name[0];
            return View();
            }
            catch
            {
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult AddInvoicingLog()
        {
            try { 
         
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection<Contract_Data> ocd =SqlQuery.ContractDataQuery(ID);
            ocd = Orderby.paiXu(ocd);
            ViewBag.Contract_DataJson = JsonTools.ObjectToJson(ocd);
            ObservableCollection<Accountant>oat=SqlQuery.AccountantQuery(ID);
            ViewBag.AccountantJson = JsonTools.ObjectToJson(oat);
                ObservableCollection<ContractNameT> os = SqlQuery.ContractVQuery(ID);
            ViewBag.ss1 = os[0].Contract_Amount;
            return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult SaveInvoicingLog( Invoicing inc)
        {

            try { 
            string s = ViewBag.Message;
            Guid ID = new Guid(Session["cc"].ToString());
           string sid= Request["ServiceID"];
            Guid ID2 = new Guid(sid);
            inc.ID = Guid.NewGuid();
            inc.LogDate = (DateTime.Now).ToString();
            inc.Contract_ID = ID;
            ObservableCollection<Contract_Data> odd= SqlQuery.Contract_DataByIDQuery(ID2);
            inc.Service = odd[0].Service;
            inc.Name = Session["username"].ToString();
            SqlQuery.insert(inc);
            ObservableCollection<Accountant> oc = SqlQuery.AccountantByServiceQuery(ID2);
            oc[0].SubInvoiceCount += inc.Count;
            oc[0].SubInvoiceAmount += inc.Amount;
            oc[0].InvoicingDate = inc.InvoicingDate;
            SqlQuery.updataAcc(oc[0]);
            return RedirectToAction("Invoicing");
        }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
      
        }
        public ActionResult InvoicingLogAjax(Invoicing inc)
        {
            try { 
           
            string s = ViewBag.Message;
            Guid ID = new Guid(Session["cc"].ToString());
            string ss = Request["ID"];
            int a = Convert.ToInt16(ss);
            ObservableCollection<Invoicing> oin = SqlQuery.Invoicing(ID,a);
            string result = JsonTools.ObjectToJson(oin);
            return Content(result);
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult InvoicingLogModification() {
            try { 
          
            Guid ID = new Guid(Session["cc"].ToString());
            ObservableCollection<Contract_Data> cd = SqlQuery.ContractDataQuery(ID);
            ViewBag.Contract_DataJson = JsonTools.ObjectToJson(cd);
            string InvoicingID = Request["ID"];
            ViewBag.ID = InvoicingID;
            Session["InvoicingID"] = InvoicingID;
            ViewBag.logName = Request["logName"];
            ViewBag.service = Request["service"];
            ViewBag.Count = Request["Count"];
            ViewBag.Amount = Request["Amount"];
            ViewBag.InDate = Request["InDate"];
            ViewBag.ServiceID = Request["ServiceID"];
            return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult SaveInvoicingLogModification(Invoicing inv)
        {
            try { 

            inv.Name= Session["username"].ToString();
            string s = Session["cc"].ToString();
            Guid ID = new Guid(s);
            s = Session["InvoicingID"].ToString();
            Guid ID2 = new Guid(s);
            ObservableCollection<Contract_Data>oc= SqlQuery.Contract_DataByIDQuery(inv.ServiceID);
            inv.Service = oc[0].Service;

            GetData.InvoicingChange(ID,inv,ID2);

            return RedirectToAction("Invoicing");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
    }
}