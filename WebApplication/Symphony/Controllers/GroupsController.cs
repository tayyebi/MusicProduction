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
    public class GroupsController : Controller
    {
        DcDataContext dc = new DcDataContext();
        // GET: Groups

        public ActionResult Index()
        {
            ViewBag.Title = "گروه ها";
            return View(dc.Groups.ToList());
        }
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            else return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                dc.Group_Insert(collection["Name"]);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "خطا در ایجاد گروه";
                if (Request.IsAjaxRequest())
                    return PartialView();
                else return View();
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            var group = dc.Groups.Where(x => x.Id == id).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(group);
            return View(group);
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                dc.Group_Update(id, collection["Name"].ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "خطا در به روز رسانی گروه";
                var group = dc.Groups.Where(x => x.Id == id).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(group);
                return View(group);
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Message = "آیا از حذف این گروه اطمینان دارید؟";
            var group = dc.Groups.Where(x => x.Id == id).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(group);
            return View(group);

        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                dc.Group_Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "خطا در حذف گروه";
                var group = dc.Groups.Where(x => x.Id == id).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(group);
                return View(group);

            }
        }
    }
}