using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicProduction.Models;
using MusicProduction.App_Code;

namespace MusicProduction.Controllers
{
	[Secure]
    public class OrdersController : Controller
    {
        DcDataContext dc = new DcDataContext();
        // GET: Purchas
        public ActionResult NewOrders()
        {
            var orders = dc.Orders.Where(x => x.مشاهده_شده == false);
            return View(orders.ToList());
        }
        public ActionResult PrevOrders()
        {
            var orders = dc.Orders.Where(x => x.مشاهده_شده == true);
            return View(orders.ToList());
        }

        public ActionResult Details(int id)
        {
            var order = dc.Orders.Where(x => x.Id == id).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(order);
            return View(order);
        }
        [HttpPost]
        public ActionResult Details(int id, string Status)
        {
            try
            {
                if (Status == "0")
                    dc.Order_Unchecked(id);
                else
                    dc.Order_Checked(id);
                return RedirectToAction("NewOrders");
            }
            catch
            {
                ViewBag.Message = "خطای سیستم";
                var order = dc.Orders.Where(x => x.Id == id).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(order);
                return View(order);
            }
        }
    }
}