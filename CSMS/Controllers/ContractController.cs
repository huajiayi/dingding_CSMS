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
using System.Configuration;

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
            try { 
            string s;
            Guid ID;
           

                ViewBag.Message = Session["cc"];
                s = ViewBag.Message;
                ID = new Guid(s);

          
            SqlQuery.DeleteContract(ID);
            
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult editContract()
        {
            try
            {
                string s;
                Guid ID;
                ViewBag.Message = Session["cc"];
                s = ViewBag.Message;
                ID = new Guid(s);
                ObservableCollection<ContractNameT> cts = SqlQuery.ContractVQuery(ID);
                decimal flag = cts[0].Contract_Amount;
                cts[0].Contract_Amount = Convert.ToDecimal(Request["Amount"]);
                ObservableCollection<Sales> osl = SqlQuery.SalesQuery(ID);
                decimal b = cts[0].Contract_Amount - flag;
                osl[0].NoAmountCollection += b;
                SqlQuery.updata(osl[0]);
                SqlQuery.updata(cts[0]);
                return Content("修改成功");
            }
            catch {
                return Content("修改失败");
            }

        }
        public ActionResult SaveContract(ContractNameT ct)
        {
            Contract_Data cd = new Contract_Data();
            try {
                ViewBag.p = "";
                cd.Service = Request["Service"];
                ct.ID = Guid.NewGuid();
                cd.ID = Guid.NewGuid();
                ct.Process = 0;
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
           catch(Exception )
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult Index()
        {
            try { 
            Session.Timeout = 120;
            ViewBag.Project = FetchValue("Project");
            ViewBag.Production = FetchValue("Production");
            ViewBag.Message = Session["userid"];
            string userid = ViewBag.Message;
            ObservableCollection<Permissions> ops = SqlQuery.PermissionsQueryByID(userid);

            if (ops.Count == 0)
            {
                ViewBag.UserJson = "";
            }
            else {
                Session["UserJson"]=ViewBag.UserJson = JsonTools.ObjectToJson(ops[0]);
                
            }
            ViewBag.p = "";
            ViewBag.ch = "";
            if (Request["ex"] != null) {
                ViewBag.p = Request["ex"];
            }
            if (Request["ch"] != null) { ViewBag.ch = Request["ch"]; } 
            
            ViewBag.ss = GetContractName();
            ViewBag.s1 = GetContractID();
            ViewBag.s2 = GetContractProcess();
            return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
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
        public static string GetContractProcess()
        {
            ObservableCollection<ContractNameT> ctt = SqlQuery.ContractQuery();

            int[] a = new int[ctt.Count];
            int i = 0;
            foreach (ContractNameT s in ctt)
            {
                a[i++] = s.Process;

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
        public ActionResult changeProcess()
        {
            try { 
            ViewBag.Message = Session["cc"];

            SqlQuery.updateProcess(Convert.ToInt16(Request["Process"]),new Guid(ViewBag.Message));
            return Content("22");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
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
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult filtration()
        {
            try {
                ObservableCollection<ContractNameT> ct = null;
                if (Request["txt_keyword"] != "" && Request["txt_startDate"] != "" && Request["txt_endDate"] != "") {
                    string txt_keyword = Request["txt_keyword"];
                    string txt_startDat = (DateTime.Parse(Request["txt_startDate"]).ToString());

                    string txt_endDate = Request["txt_endDate"];

                    ct = SqlQuery.ContractVQueryByDateandName(txt_keyword, txt_startDat, txt_endDate, 0);
                }
                if (Request["txt_keyword"] != "" && Request["txt_startDate"] == "" && Request["txt_endDate"] == "") {
                    string txt_keyword = Request["txt_keyword"];
                    ct = SqlQuery.ContractVQueryByName(txt_keyword, 0);

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
                string a = GetContractName(ct);
                ViewBag.ss = a;
                ViewBag.s1 = GetContractID(ct);
                ViewBag.Project = FetchValue("Project");
                ViewBag.Production = FetchValue("Production");
                ViewBag.s2 = GetContractProcess();
                if (a == "[]") {
                    ViewBag.p = "没有符合条件的合同";
                }
                ViewBag.UserJson = Session["UserJson"];
                return View("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult filtrationAjax()
        {
            try {
                string a2 = Request["txt_keyword"];
                string b = Request["txt_startDate"];
                string c = Request["txt_endDate"];
                string n = Request["ID"];
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
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult ChangeStats() {

            try { 
            List<double[]> ld = new List<double[]>();
            ld.Add(GetData.GetYOYincrease(Request["type"],1));
            ld.Add(GetData.SIncreaseRate(Request["type"],1));
            ld.Add(GetData.GetYOYincrease(Request["type"], 2));
            ld.Add(GetData.SIncreaseRate(Request["type"], 2));
            ld.Add(GetData.GetYOYincrease(Request["type"], 3));
            ld.Add(GetData.SIncreaseRate(Request["type"], 3));
            ld.Add(GetData.GetYOYincrease(Request["type"], 4));
            ld.Add(GetData.SIncreaseRate(Request["type"], 4));
            return Content(JsonTools.ObjectToJson(ld));
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult Stats() {
            try
            {
                ObservableCollection<SalesChart> osc = SqlQuery.DoSalesChart();
                ViewBag.NoTotalRevenue = osc[0].NoTotalRevenue;
                ViewBag.TotalRevenue = osc[0].TotalRevenue;
                decimal[] Ar = { SqlQuery.InvoiceAmountChartInit(), SqlQuery.AmountChartInit(), osc[0].NoTotalRevenue, osc[0].TotalRevenue };
                ViewBag.Ar = JsonTools.ObjectToJson(Ar);
                ObservableCollection<ProductionerChart> opc = SqlQuery.DoProductionerChart();
                ViewBag.SumNoTotalProduct = opc[0].SumNoTotalProduct;
                ViewBag.SumTotalProduct = opc[0].SumTotalProduct;
                ObservableCollection<WarehouseChart> owc = SqlQuery.DoWarehouseChart();
                ViewBag.SumNoShippedCount = owc[0].SumNoShippedCount;
                ViewBag.SumReserves = owc[0].SumReserves;
                ViewBag.SumShippedCount = owc[0].SumShippedCount;
                double[] Ar2 = { opc[0].SumNoTotalProduct, opc[0].SumTotalProduct, owc[0].SumNoShippedCount, owc[0].SumReserves, owc[0].SumShippedCount, SqlQuery.ContractGetCount() };
                //ObservableCollection<Stats> oss = SqlQuery.StatsQuery();
                //string[] cn = new string[oss.Count];
                //decimal[] ona=new decimal[oss.Count];
                //decimal[] oa= new decimal[oss.Count];
                //double[] otp= new double[oss.Count];
                //double[] ontp= new double[oss.Count];
                //double[] os= new double[oss.Count];
                //double[] ons=new double[oss.Count];
                //for (int i=0;i<oss.Count;i++) {
                //    cn[i] = oss[i].ContractName;
                //    ona[i] = oss[i].NoAmountCollection;
                //    oa[i] = oss[i].SubAffirmIncomeAmount;
                //    otp[i] = oss[i].TotalProduct;
                //    ontp[i] = oss[i].NoTotalProduct;
                //    os[i] = oss[i].ShippedCount;
                //    ons[i] = oss[i].NoShippedCount;
                //}
                //ViewBag.ContractName= JsonTools.ObjectToJson(cn);
                //ViewBag.NoAmountCollection = JsonTools.ObjectToJson(ona);
                //ViewBag.SubAffirmIncomeAmount = JsonTools.ObjectToJson(oa);
                //ViewBag.TotalProduct = JsonTools.ObjectToJson(otp);
                //ViewBag.NoTotalProduct = JsonTools.ObjectToJson(ontp);
                //ViewBag.ShippedCount = JsonTools.ObjectToJson(os);
                //ViewBag.NoShippedCount = JsonTools.ObjectToJson(ons);
                ViewBag.Ar2 = JsonTools.ObjectToJson(Ar2);
                ViewBag.YOYincrease = JsonTools.ObjectToJson(GetData.GetYOYincrease(1));
                ViewBag.SIncreaseRate = JsonTools.ObjectToJson(GetData.SIncreaseRate(1));
                ViewBag.YOYincrease2 = JsonTools.ObjectToJson(GetData.GetYOYincrease(2));
                ViewBag.SIncreaseRate2 = JsonTools.ObjectToJson(GetData.SIncreaseRate(2));
                ViewBag.YOYincrease3 = JsonTools.ObjectToJson(GetData.GetYOYincrease(3));
                ViewBag.SIncreaseRate3 = JsonTools.ObjectToJson(GetData.SIncreaseRate(3));
                ViewBag.YOYincrease4 = JsonTools.ObjectToJson(GetData.GetYOYincrease(4));
                ViewBag.SIncreaseRate4 = JsonTools.ObjectToJson(GetData.SIncreaseRate(4));

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        public ActionResult DoStats()
        {
            try {
                string Start = Request["Start"];
                string End = Request["End"];
                string Typ = Request["Typ"];
                ObservableCollection<SalesChart> osct = null;

                decimal[] aa = new decimal[10];

                if (Typ == "")
                {
                    aa[2] = SqlQuery.ContractGetNoAmountCollection(Start, End);
                    aa[0] = SqlQuery.InvoiceAmountChart(Start, End);
                    aa[3] = SqlQuery.AffirmIncomeAmountChart(Start, End);
                    aa[1] = SqlQuery.ContractGetAmount(Start, End);
                    aa[5] = Convert.ToDecimal(SqlQuery.DoProductionerChart2(Start, End));
                    aa[4] = Convert.ToDecimal(SqlQuery.DoProductionerChart(Start, End));
                    ObservableCollection<WarehouseChart> ow = SqlQuery.DoWarehouseChart2(Start, End);
                    ObservableCollection<WarehouseChart> oww = SqlQuery.DoWarehouseChart(Start, End);
                    aa[8] = Convert.ToDecimal(ow[0].SumShippedCount);
                    aa[7] = Convert.ToDecimal(oww[0].SumReserves);
                    aa[6] = Convert.ToDecimal(oww[0].SumNoShippedCount);
                    aa[9] = Convert.ToDecimal(SqlQuery.ContractGetCount(Start, End));
                }
                else {

                    aa[2] = SqlQuery.ContractGetNoAmountCollectionByType(Start, End, Typ);
                    aa[0] = SqlQuery.InvoiceAmountChart(Start, End, Typ);
                    aa[3] = SqlQuery.AffirmIncomeAmountChart(Start, End, Typ);
                    aa[1] = SqlQuery.ContractGetAmountByType(Start, End, Typ);
                    aa[5] = Convert.ToDecimal(SqlQuery.DoProductionerChart2(Start, End, Typ));
                    aa[4] = Convert.ToDecimal(SqlQuery.DoProductionerChart(Start, End, Typ));
                    ObservableCollection<WarehouseChart> ow = SqlQuery.DoWarehouseChart2(Start, End, Typ);
                    ObservableCollection<WarehouseChart> oww = SqlQuery.DoWarehouseChart(Start, End, Typ);
                    aa[8] = Convert.ToDecimal(ow[0].SumShippedCount);
                    aa[7] = Convert.ToDecimal(oww[0].SumReserves);
                    aa[6] = Convert.ToDecimal(oww[0].SumNoShippedCount);
                    aa[9] = Convert.ToDecimal(SqlQuery.ContractGetCountByType(Start, End, Typ));
                }


                return Content(JsonTools.ObjectToJson(aa));
            }
            catch (Exception)
            {
                return RedirectToAction("noPremission", "FirstPage", new { ex = "操作异常或超时已退回首页请刷新重试" });
            }
        }
        private static String FetchValue(String key)
        {

            String value = ConfigurationManager.AppSettings[key];
            if (value == null)
            {
                throw new Exception($"{key} is null.请确认配置文件中是否已配置.");
            }
            return value;
        }
    }
}