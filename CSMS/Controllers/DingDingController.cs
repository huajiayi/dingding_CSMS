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
using System.Web.Caching;
using System.Threading.Tasks;
namespace WebApplication4.Controllers
{
    public class DingDingController : Controller
    {
        // GET: DingDing
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetSignPackage()
        {

            AccessToken token = null;
            ObservableCollection<AccessToken> oat = SqlQuery.AccessTokenQuery();
            if (oat[0].Value == "0" || oat[0].Begin.AddSeconds(7000) < DateTime.Now)
            {
                AccessTokenGet.UpdateAccessToken();
                SqlQuery.updata(AccessTokenGet.AccessToken);
                token = AccessTokenGet.AccessToken;
            }
            else {
                token = oat[0];
            }
            Session["Token"] = token.Value;
            var signPackage = SignGet.FetchSignPackage(Request["url"], token);

            return Content(JsonTools.ObjectToJson(signPackage));
        }
        public ActionResult GetuserId()
        {
            try
            {
                string CODE = Request["code"];
                string s = Session["Token"].ToString();
                string TokenUrl = "https://oapi.dingtalk.com/user/getuserinfo";
                string apiurl = $"{TokenUrl}?access_token={s}&code={CODE}";
                WebRequest request = WebRequest.Create(@apiurl);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.UTF8;
                StreamReader reader = new StreamReader(stream, encode);
                string resultJson = reader.ReadToEnd();
                return Content(resultJson);
            }
            catch(Exception e) {
                return Content(e.Message);
            }
        }
        public ActionResult Getuser()
        {
            try {
                string userid = Request["userid"];

                ViewBag.Message = Session["Token"];
                Session["userid"] = userid;
                string s = ViewBag.Message;
                string TokenUrl = "https://oapi.dingtalk.com/user/get";
                string apiurl = $"{TokenUrl}?access_token={s}&userid={userid}";
                WebRequest request = WebRequest.Create(@apiurl);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.UTF8;
                StreamReader reader = new StreamReader(stream, encode);
                string resultJson = reader.ReadToEnd();
                ObservableCollection<string> obc = new ObservableCollection<string>();
                ObservableCollection<Permissions> ops = SqlQuery.PermissionsQueryByID(userid);

                if (ops.Count == 0)
                {
                   
                    obc.Add(resultJson);
                    obc.Add("0");
                }
                else
                {
                    ObservableCollection<Permissions> p = SqlQuery.PermissionsQueryByID(userid);
                    Session["username"] = p[0].Name;
                    obc.Add(resultJson);
                    obc.Add("1");
                }

                return Content(JsonTools.ObjectToJson(obc));
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        public ActionResult Getdepartment()
        {

           
            ViewBag.Message = Session["Token"];
            string s = ViewBag.Message;
            string TokenUrl = "https://oapi.dingtalk.com/department/list";
            string apiurl = $"{TokenUrl}?access_token={s}";
            WebRequest request = WebRequest.Create(@apiurl);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string resultJson = reader.ReadToEnd();
            return Content(resultJson);


        }
        public ActionResult GetSign()
        {
            string s = Session["Token"].ToString();
            string TokenUrl = "https://oapi.dingtalk.com/department/list";
            string apiurl = $"{TokenUrl}?access_token={s}";
            WebRequest request = WebRequest.Create(@apiurl);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string resultJson = reader.ReadToEnd();
            return Content(resultJson);
        }
        public ActionResult GetCid()
        {


           
            string s = Session["Token"].ToString();
            string TokenUrl = "https://oapi.dingtalk.com/message/send_to_conversation";
            string apiurl = $"{TokenUrl}?access_token={s}";
            WebRequest request = WebRequest.Create(@apiurl);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.UTF8;
            StreamReader reader = new StreamReader(stream, encode);
            string resultJson = reader.ReadToEnd();
            return Content(resultJson);


        }
        public ActionResult DeleteCache()
        {
           
            //if (HttpContext.Cache["Token"] != null)
            //{
            //    HttpContext.Cache.Remove("Token");
                
            //}if (HttpContext.Cache["ticket"] != null)
            //    {
            //    HttpContext.Cache.Remove("ticket");
            //}
            return Content(JsonTools.ObjectToJson(""));

        }
        public ActionResult GetExcel()
        {
            GetData.GetExcel();
            return Content(JsonTools.ObjectToJson(""));
        }
    }
}