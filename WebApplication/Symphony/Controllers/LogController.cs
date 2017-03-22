using MusicProduction.App_Code;
using MusicProduction.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicProduction.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult In()
        {
            return View();
        }

        [HttpPost]
        public ActionResult In(string Username, string Password)
        {

            DcDataContext dc = new DcDataContext();
            if (dc.Admin_Login(Username, Password).Count() > 0)
            {
                Session["Username"] = Username;
                Session["Password"] = Password;
            }
            else
            {
                ViewBag.Message = "نام کاربری یا کلمه عبور اشتباه است";
                return View();
            }
            return RedirectToAction("Index", "Admin");
        }
        [Secure]
        public ActionResult Out()
        {
            Session["Username"] = null;
            Session["Password"] = null;
            return RedirectToAction("In", "Log");
        }
    }
}