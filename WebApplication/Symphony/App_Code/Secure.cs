using MusicProduction.Controllers;
using MusicProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MusicProduction.App_Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]

    class SecureAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool CanAccess = true;
            try
            {
                DcDataContext dc = new DcDataContext();
                if (dc.Admin_Login(filterContext.HttpContext.Session["Username"].ToString(), filterContext.HttpContext.Session["Password"].ToString()).Count() > 0)
                {
                    CanAccess = true;
                }
            }
            catch
            {
                CanAccess = false;
            }
            if (!CanAccess)
                filterContext.Result = new HttpStatusCodeResult(404);
        }
    }
}
