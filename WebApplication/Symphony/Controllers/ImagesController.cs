using MusicProduction.App_Code;
using MusicProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MusicProduction.Controllers
{
    [Secure]
    public class ImagesController : Controller
    {
        DcDataContext dc = new DcDataContext();
        // GET: Files
        public ActionResult Index(int Product = 0, int Post = 0)
        {
            var images = dc.Images.Select(m => new MusicProduction.Models.Image { عکس = m.عکس, Id = m.Id });

            bool filtered = false;

            if (Post != 0)
            {
                images = images.Where(x => x.Post_Id == Post);
            }
            if (Product != 0)
            {
                images = images.Where(x => x.Product_Id == Product);
            }

            if (filtered)
            {
                ViewBag.Message = "شما در حال مشاهده برخی از تصاویر هستید";
            }

            if (Request.IsAjaxRequest())
                return PartialView(images.ToList());
            return View(images.ToList());
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string Name,string Description, HttpPostedFileBase Parvandeh, int Product = 0, int Post = 0)
        {
            try
            {
                var _Bytes = new byte[Parvandeh.ContentLength];
                Parvandeh.InputStream.Read(_Bytes, 0, Parvandeh.ContentLength);

                dc.Image_Insert(null, Name, _Bytes, Description, Product, Post);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Message = "خطا در بارگزاری فایل";
                if (Request.IsAjaxRequest())
                    return PartialView();
                return View();
            }
        }

        // GET: Files/Edit/5
        public ActionResult Edit(Guid? id)
        {
            var image = dc.Images.Where(x => x.Id == id).Select(m => new MusicProduction.Models.Image { عکس = m.عکس, Id = m.Id, توضیحات = m.توضیحات }).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(image);
            return View(image);
        }

        // POST: Files/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid? id, string Name, string Description, int Product = 0, int Post = 0, HttpPostedFileBase Parvandeh = null)
        {
            try
            {
                if (Parvandeh == null)
                {
                    dc.Image_Edit(id, Name, null, Description, Product, Post);
                }
                else
                {
                    var _Bytes = new byte[Parvandeh.ContentLength];
                    Parvandeh.InputStream.Read(_Bytes, 0, Parvandeh.ContentLength);
                    dc.Image_Edit(id, Name, _Bytes, Description, null, null);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "خطا در به روز رسانی";
                var image = dc.Images.Where(x => x.Id == id).Select(m => new MusicProduction.Models.Image { عکس = m.عکس, Id = m.Id, توضیحات = m.توضیحات }).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(image);
                return View(image);
            }
        }

        // GET: Files/Delete/5
        public ActionResult Delete(Guid? id)
        {
            var image = dc.Images.Where(x => x.Id == id).Select(m => new MusicProduction.Models.Image { عکس = m.عکس, Id = m.Id, توضیحات = m.توضیحات }).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(image);
            return View(image);
        }

        // POST: Files/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid? id, FormCollection collection)
        {
            try
            {
                dc.Image_Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "خطا در حذف فایل";
                var image = dc.Images.Where(x => x.Id == id).Select(m => new MusicProduction.Models.Image { عکس = m.عکس, Id = m.Id, توضیحات = m.توضیحات }).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(image);
                return View(image);
            }
        }
    }
}