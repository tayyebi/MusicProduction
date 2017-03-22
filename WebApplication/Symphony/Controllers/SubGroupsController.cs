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
    public class SubGroupsController : Controller
    {
        DcDataContext dc = new DcDataContext();

        // GET: SubGroups
        public ActionResult Index(int group)
        {
            return View(dc.SubGroups.Where(x => x.GroupId == group).ToList());
        }

        // GET: SubGroups/Create
        public ActionResult Create(int group)
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: SubGroups/Create
        [HttpPost]
        public ActionResult Create(int group, string Name)
        {
            try
            {
                dc.SubGroup_Insert(group, Name);
                return RedirectToAction("Index", new { group = group });
            }
            catch
            {
                return View();
            }
        }

        // GET: SubGroups/Edit/5
        public ActionResult Edit(int id, int group)
        {
            var subGroup = dc.SubGroups.Where(x => x.GroupId == group).Where(x => x.Id == id).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(subGroup);
            return View(subGroup);
        }

        // POST: SubGroups/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, int group, string Name)
        {
            try
            {
                dc.SubGroup_Update(id, group, Name);
                return RedirectToAction("Index", new { group = group });
            }
            catch
            {
                var subGroup = dc.SubGroups.Where(x => x.GroupId == group).Where(x => x.Id == id).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(subGroup);
                return View(subGroup);
            }
        }

        // GET: SubGroups/Delete/5
        public ActionResult Delete(int id, int group)
        {
            var subGroup = dc.SubGroups.Where(x => x.GroupId == group).Where(x => x.Id == id).FirstOrDefault();
            if (Request.IsAjaxRequest())
                return PartialView(subGroup);
            return View(subGroup);
        }

        // POST: SubGroups/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int group, string Name)
        {
            try
            {
                dc.SubGroup_Delete(id);
                return RedirectToAction("Index", new { group = group });
            }
            catch
            {
                var subGroup = dc.SubGroups.Where(x => x.GroupId == group).Where(x => x.Id == id).FirstOrDefault();
                if (Request.IsAjaxRequest())
                    return PartialView(subGroup);
                return View(subGroup);
            }
        }
    }
}
