using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class FirstPageController : Controller
    {
        // GET: FristPage
        public ActionResult noPremission()
        {
            ViewBag.p = "";
            if (Request["ex"] != null)
            {
                ViewBag.p = Request["ex"];
            }
            Session.Timeout = 120;
            
            return View();
        }
    }
}