using MusicProduction.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicProduction.Controllers
{
    public class AdminController : Controller
    {
        [Secure]
        // GET: Admin
        public ActionResult Index()
        {
                ViewBag.Message = "مدیر " + HttpContext.Session["Username"] + " ، خوش آمدید!";
            return View();
        }
    }
}