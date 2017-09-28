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
            return View();
        }
    }
}