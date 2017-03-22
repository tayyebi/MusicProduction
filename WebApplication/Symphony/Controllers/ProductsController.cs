using MusicProduction.App_Code;
using MusicProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicProduction.Controllers
{
    [Secure]
    public class ProductsController : Controller
    {
        DcDataContext dc = new DcDataContext();
        // GET: Products
        public ActionResult Index()
        {
            var products = dc.Products.ToList();
            if (Request.IsAjaxRequest())
                return PartialView(products);
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase Parvandeh)
        {
            try
            {
                var _Bytes = new byte[Parvandeh.ContentLength];
                Parvandeh.InputStream.Read(_Bytes, 0, Parvandeh.ContentLength);
                dc.Product_Insert(collection["Name"], _Bytes, collection["Abstract"], collection["Description"], Convert.ToInt32(collection["SubGroup"]));
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = "خطا در تعریف محصول جدید";
                if (Request.IsAjaxRequest())
                    return PartialView();
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var product = dc.Products.Where(x => x.Id == id).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(product);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase Parvandeh)
        {
            try
            {
                if (Parvandeh == null)
                {
                    dc.Product_Edit(id, collection["Name"], null, collection["Abstract"], collection["Description"], Convert.ToInt32(collection["SubGroup"]));
                }
                else
                {
                    var _Bytes = new byte[Parvandeh.ContentLength];
                    Parvandeh.InputStream.Read(_Bytes, 0, Parvandeh.ContentLength);
                    dc.Product_Edit(id, collection["Name"], _Bytes, collection["Abstract"], collection["Description"], Convert.ToInt32(collection["SubGroup"]));
                }
                return RedirectToAction("Index");
            }
            catch
            {
                var product = dc.Products.Where(x => x.Id == id).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(product);
                return View(product);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var product = dc.Products.Where(x => x.Id == id).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(product);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                dc.Product_Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                var product = dc.Products.Where(x => x.Id == id).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(product);
                return View(product);
            }
        }
    }
}
