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
using System.Net;
using System.IO;

namespace WebApplication4.Controllers
{
    public class ContractController : Controller
    {
       
        // GET: MM66
        public ActionResult AddContract()
        {
            return View();
        }
        public ActionResult deleteContract()
        {
            string s;
            Guid ID;
           

                ViewBag.Message = Session["cc"];
                s = ViewBag.Message;
                ID = new Guid(s);

          
            SqlQuery.DeleteContract(ID);
            
            return RedirectToAction("Index");
        }
        public ActionResult SaveContract(ContractNameT ct)
        {
            Contract_Data cd = new Contract_Data();
            try {
                ViewBag.p = "";
                cd.Service = Request["Service"];
                ct.ID = Guid.NewGuid();
                cd.ID = Guid.NewGuid();
                cd.Contract_ID = ct.ID;
                GetData.first(ct, cd);
                SqlQuery.insert(cd);
                string s = Request["I"];
                int j = Convert.ToInt16(s);
                string[] Services = new string[j];
                int i = 1;
                while (j != 0 && i <= j)
                {
                    cd.ID = Guid.NewGuid();
                    cd.Contract_ID = ct.ID;
                    cd.Service = Request["txt_service" + i++];
                    SqlQuery.insert(cd);

                }
                //cd.Contract_ID = ct.ID;
                //ViewBag.ss = ss();
                //ViewBag.s1 = cd.Service;
                //s1 = "1";
                ObservableCollection<Contract_Data> cd2 = SqlQuery.ContractDataQuery(ct.ID);
                foreach (Contract_Data cdd in cd2)
                {
                    Accountant att = new Accountant();
                    att.ContractID = ct.ID;
                    att.ID = Guid.NewGuid();
                    att.ServiceID = cdd.ID;
                    att.Service = cdd.Service;
                    att.NoAffirmIncomeAmount = ct.Contract_Amount;
                    att.SubAffirmIncomeAmount = 0;
                    att.SubInvoiceAmount = 0;
                    att.SubInvoiceCount = 0;
                    att.SubManufacturing_Costs = 0;
                    att.SubMaterial = 0;
                    att.Subtotal = 0;
                    att.Subworker = 0;
                    att.SubCost = 0;
                    att.AffirmIncomeGist = "未确认收入";
                    Project_data pd2 = new Project_data();
                    pd2.ServiceID = cdd.ID;
                    pd2.ID = Guid.NewGuid();
                    pd2.ContractID = ct.ID;
                    pd2.Service = cdd.Service;
                    pd2.ProjectStart = " ";
                    pd2.DompletedDate = " ";
                    pd2.DompletedAcceptanceDate = " ";
                    SqlQuery.insert(pd2);
                    SqlQuery.insert(att);
                }
                return RedirectToAction("Index",new { ch = ct.ContractName });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult Index()
        {
            ViewBag.Message = Session["userid"];
            string userid = ViewBag.Message;
            ObservableCollection<Permissions> ops = SqlQuery.PermissionsQueryByID(userid);

            if (ops.Count == 0)
            {
                ViewBag.UserJson = "";
            }
            else { ViewBag.UserJson = JsonTools.ObjectToJson(ops[0]); }
            ViewBag.p = "";
            ViewBag.ch = "";
            if (Request["ex"] != null) {
                ViewBag.p = Request["ex"];
            }
            if (Request["ch"]!=null) { ViewBag.ch = Request["ch"]; }
            
            ViewBag.ss = GetContractName();
            ViewBag.s1 = GetContractID();
            return View();
        }
      
        public static string GetContractName() {
            ObservableCollection<ContractNameT> ctt = SqlQuery.ContractQuery();

            string[] a = new string[ctt.Count];
            int i = 0;
            foreach (ContractNameT s in ctt)
            {
                a[i++] = s.ContractName;

            }
            return JsonConvert.SerializeObject(a);
        }
        public static string GetContractName(ObservableCollection<ContractNameT> ctt)
        {

            string[] a = new string[ctt.Count];
            int i = 0;
            foreach (ContractNameT s in ctt)
            {
                a[i++] = s.ContractName;

            }
            return JsonConvert.SerializeObject(a);
        }
        public static string GetContractID(ObservableCollection<ContractNameT> ctt)
        {
           
            Guid[] a = new Guid[ctt.Count];
            int i = 0;
            foreach (ContractNameT s in ctt)
            {
                a[i++] = s.ID;

            }
            return JsonConvert.SerializeObject(a);
        }
        public static string GetContractID()
        {
            ObservableCollection<ContractNameT> ctt = SqlQuery.ContractQuery();
            Guid[] a = new Guid[ctt.Count];
            int i = 0;
            foreach (ContractNameT s in ctt)
            {
                a[i++] = s.ID;

            }
            return JsonConvert.SerializeObject(a);
        }
        public ActionResult ContractContent()
        {
            try
            {
                string s;
                Guid ID;

                ViewBag.p = "";
                if (Request["ID"] == null)
                {
                    ViewBag.Message = Session["cc"];

                    s = ViewBag.Message;
                    ID = new Guid(s);
                }
                else {
                    s = Request["ID"];
                    ID = new Guid(s);
                }
                ViewBag.Message = Session["userid"];
                string userid = ViewBag.Message;

                ObservableCollection<Permissions> ops = SqlQuery.PermissionsQueryByID(userid);

                if (ops.Count == 0)
                {
                    ViewBag.UserJson = "";
                }
                else { ViewBag.UserJson = JsonTools.ObjectToJson(ops[0]); }
                ObservableCollection<ContractNameT> ct = SqlQuery.ContractVQuery(ID);
                ObservableCollection<Contract_Data> cd = SqlQuery.ContractDataQuery(ID);
                cd = Orderby.paiXu(cd);
                string[] Services = new string[cd.Count];
                int i = 0;
                foreach (Contract_Data cda in cd)
                {
                    Services[i++] = cda.Service;
                }
                ViewBag.ServicesJson = JsonTools.ObjectToJson(Services);

                ContractNameT cc = ct[0];
                Session["cc"] = s;
                //ViewContext c = new ViewContext();
                //c.ViewData = new ViewDataDictionary();
                //c.ViewData.Add("cc", s);
                //ViewBag.h = ID.ToString();
                return View(cc);
            }
            catch
            {
                return RedirectToAction("Index", "Contract", new { ex = "操作异常已退回首页请刷新重试" });
            }
        }
        public ActionResult filtration()
        {

            ObservableCollection<ContractNameT> ct = null;
            if (Request["txt_keyword"] != "" && Request["txt_startDate"] != "" && Request["txt_endDate"] != "") {
                string txt_keyword = Request["txt_keyword"];
               string txt_startDat = (DateTime.Parse(Request["txt_startDate"]).ToString());
                
                string txt_endDate = Request["txt_endDate"];
                
                ct =SqlQuery.ContractVQueryByDateandName(txt_keyword, txt_startDat, txt_endDate,0);
            }
            if (Request["txt_keyword"] != "" && Request["txt_startDate"] == "" && Request["txt_endDate"] == "") {
                string txt_keyword = Request["txt_keyword"];
                ct = SqlQuery.ContractVQueryByName(txt_keyword,0);
                System.IO.File.WriteAllText(@"D:\testDir\test1.txt", txt_keyword, Encoding.UTF8);
            }
            if (Request["txt_keyword"] == "" && Request["txt_startDate"] != "" && Request["txt_endDate"] != "")
            {
                string txt_startDat = Request["txt_startDate"];
                string txt_endDate = Request["txt_endDate"];
                 ct = SqlQuery.ContractVQueryByDate(txt_startDat, txt_endDate, 0);
            }
            if (Request["txt_keyword"] == "" && Request["txt_startDate"] == "" && Request["txt_endDate"] == "")
            {
                return RedirectToAction("Index");
            }
            ViewBag.ss = GetContractName(ct);
            ViewBag.s1 = GetContractID(ct);
            return View("Index");
        }
        public ActionResult filtrationAjax()
        {
            string a2 = Request["txt_keyword"];
            string b = Request["txt_startDate"];
            string c = Request["txt_endDate"];
            string n=Request["ID"];
            int a = Convert.ToInt16(n);
            ObservableCollection<ContractNameT> ct = null;
            if (Request["txt_keyword"] != "" && Request["txt_startDate"] != "" && Request["txt_endDate"] != "")
            {
                string txt_keyword = Request["txt_keyword"];
                string txt_startDat = Request["txt_startDate"];
                string txt_endDate = Request["txt_endDate"];
                ct = SqlQuery.ContractVQueryByDateandName(txt_keyword, txt_startDat, txt_endDate, a);
            }
            if (Request["txt_keyword"] != "" && Request["txt_startDate"] == "" && Request["txt_endDate"] == "")
            {
                string txt_keyword = Request["txt_keyword"];
                ct = SqlQuery.ContractVQueryByName(txt_keyword, a);
            }
            if (Request["txt_keyword"] == "" && Request["txt_startDate"] != "" && Request["txt_endDate"] != "")
            {
                string txt_startDat = Request["txt_startDate"];
                string txt_endDate = Request["txt_endDate"];
                ct = SqlQuery.ContractVQueryByDate(txt_startDat, txt_endDate, a);
            }
            if (Request["txt_keyword"] == "" && Request["txt_startDate"] == "" && Request["txt_endDate"] == "")
            {

                ct = SqlQuery.ContractQuery(a);
            }
            string result = JsonTools.ObjectToJson(ct);
            return Content(result);
          
        }
        public ActionResult Stats() {

            ObservableCollection <SalesChart> osc= SqlQuery.DoSalesChart();
            ViewBag.NoTotalRevenue = osc[0].NoTotalRevenue;
            ViewBag.TotalRevenue = osc[0].TotalRevenue;
            ObservableCollection<ProductionerChart> opc= SqlQuery.DoProductionerChart();
            ViewBag.SumNoTotalProduct = opc[0].SumNoTotalProduct;
            ViewBag.SumTotalProduct = opc[0].SumTotalProduct;
            ObservableCollection<WarehouseChart> owc= SqlQuery.DoWarehouseChart();
            ViewBag.SumNoShippedCount = owc[0].SumNoShippedCount;
            ViewBag.SumReserves = owc[0].SumReserves;
            ViewBag.SumShippedCount=owc[0].SumShippedCount;   
            ObservableCollection<Stats> oss = SqlQuery.StatsQuery();

            string[] cn = new string[oss.Count];
            decimal[] ona=new decimal[oss.Count];
            decimal[] oa= new decimal[oss.Count];
            double[] otp= new double[oss.Count];
            double[] ontp= new double[oss.Count];
            double[] os= new double[oss.Count];
            double[] ons=new double[oss.Count];
            for (int i=0;i<oss.Count;i++) {
                cn[i] = oss[i].ContractName;
                ona[i] = oss[i].NoAmountCollection;
                oa[i] = oss[i].SubAffirmIncomeAmount;
                otp[i] = oss[i].TotalProduct;
                ontp[i] = oss[i].NoTotalProduct;
                os[i] = oss[i].ShippedCount;
                ons[i] = oss[i].NoShippedCount;
            }
            ViewBag.ContractName= JsonTools.ObjectToJson(cn);
            ViewBag.NoAmountCollection = JsonTools.ObjectToJson(ona);
            ViewBag.SubAffirmIncomeAmount = JsonTools.ObjectToJson(oa);
            ViewBag.TotalProduct = JsonTools.ObjectToJson(otp);
            ViewBag.NoTotalProduct = JsonTools.ObjectToJson(ontp);
            ViewBag.ShippedCount = JsonTools.ObjectToJson(os);
            ViewBag.NoShippedCount = JsonTools.ObjectToJson(ons);
            //ViewBag.YOYincrease = JsonTools.ObjectToJson(GetData.GetYOYincrease());
            //ViewBag.SIncreaseRate = JsonTools.ObjectToJson(GetData.SIncreaseRate());
            return View();
        }
        public ActionResult DoStats()
        {
            string Start = Request["Start"];
            string End = Request["End"];
            string Typ = Request["Typ"];
            ObservableCollection<SalesChart> osct = null;
            if (Typ == "")
            {
                osct = SqlQuery.DoSalesChartByDate(Start, End);
            }
            else {
                osct = SqlQuery.DoSalesChartByDate(Start, End, Typ);
            }
          
            return Content(JsonTools.ObjectToJson(osct[0]));
        }
    }
}