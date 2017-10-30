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
            Cache cache =new Cache();
            if (cache["Token"] == null)
            {
                AccessTokenGet.UpdateAccessToken();
                cache.Add("Token", AccessTokenGet.AccessToken, null, DateTime.Now.AddSeconds(7200), TimeSpan.Zero, CacheItemPriority.Normal, null); ;
            }
            AccessToken token = (AccessToken)cache["Token"];
            Session["Token"] = token.Value;
            var signPackage = SignGet.FetchSignPackage(Request["url"], token);
            return Content(JsonTools.ObjectToJson(signPackage));
        }
        public ActionResult GetuserId()
        {
            try
            {
                string CODE = Request["code"];
                ViewBag.Message = Session["Token"];
                string s = ViewBag.Message;
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
        public ActionResult GetCid()
        {


            ViewBag.Message = Session["Token"];
            string s = ViewBag.Message;
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
            Cache cache = new Cache();
            if (cache["Token"] != null)
            {
                cache.Remove("Token");
                if (cache["ticket"] != null)
                {
                    cache.Remove("ticket");
                }
            }
            return Content(JsonTools.ObjectToJson(""));

        }
    }
}