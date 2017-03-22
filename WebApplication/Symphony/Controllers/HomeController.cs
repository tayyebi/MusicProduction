using MusicProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicProduction.Controllers
{
    public class HomeController : Controller
    {
        DcDataContext dc = new DcDataContext();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "خانه";
            return View();
        }
        public ActionResult Error()
        {
            ViewBag.Message = "سرور با خطا مواجه شده است";
            ViewBag.Title = "خطا";
            return View();
        }
    }
}