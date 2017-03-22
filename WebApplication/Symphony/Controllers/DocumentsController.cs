using MusicProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MusicProduction.Controllers
{
    public class DocumentsController : Controller
    {
        DcDataContext dc = new DcDataContext();
        public ActionResult Download(Guid id)
        {
            try {
                File file;
                file = dc.Files.Where(y => y.Id == id).FirstOrDefault();

                if (id == null) return HttpNotFound();

                var _Bytes = new byte[file.حجم];
                _Bytes = file.Content.ToArray();

                return File(_Bytes, file.نوع, file.عنوان);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        public ActionResult Post(int id)
        {
            var post = dc.Posts.Where(x => x.Id == id);
            if (Request.IsAjaxRequest())
                return PartialView("Post", post.Select(m => new MusicProduction.Models.Post { متن = m.متن }).FirstOrDefault());
            else
                return View(post.FirstOrDefault());
        }
        public ActionResult Product(int id)
        {
            return View(dc.Products.Where(x => x.Id == id).FirstOrDefault());
        }


        public ActionResult Purchase()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
        [HttpPost]
        public ActionResult Purchase(FormCollection collection)
        {
            try
            {
                dc.Order_Insert(Convert.ToInt32(collection["Product"]), Convert.ToInt32(collection["Count"]), collection["FullName"], collection["Email"], collection["Mobile"], collection["Address"], collection["Description"]);
                ViewBag.Message = "سفارش شما با موفقیت ثبت شد! ♥";
            }
            catch (Exception e)
            {
                ViewBag.Message = "فرایند ثبت سفارش با مشکل رو به رو شد؛ لطفا با پشتیبانی سایت تماس بگیرید";
            }
            if (Request.IsAjaxRequest())
                return PartialView();
            else return View();

        }
    }
}
