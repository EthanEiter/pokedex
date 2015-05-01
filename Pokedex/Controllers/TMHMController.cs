using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pokedex.DAL;
using Pokedex.Models;
using PagedList;

namespace Pokedex.Controllers
{
    public class TMHMController : Controller
    {
        private DexContext db = new DexContext();

        // GET: TMHM
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tmhms = from tm in db.TMHMs
                           select tm;

            if (!String.IsNullOrEmpty(searchString))
            {
                tmhms = tmhms.Where(tm => (tm.TMHMID + tm.Name).Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    tmhms = tmhms.OrderBy(tm => tm.Name);
                    break;
                case "name_desc":
                    tmhms = tmhms.OrderByDescending(tm => tm.Name);
                    break;
                case "type":
                    tmhms = tmhms.OrderBy(tm => tm.Type);
                    break;
                case "type_desc":
                    tmhms = tmhms.OrderByDescending(tm => tm.Type);
                    break;
                default:
                    tmhms = tmhms.OrderBy(tm => tm.TMHMID);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(tmhms.ToPagedList(pageNumber, pageSize));
        }

        // GET: TMHM/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMHM tMHM = db.TMHMs.Find(id);
            if (tMHM == null)
            {
                return HttpNotFound();
            }
            return View(tMHM);
        }

        // GET: TMHM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TMHM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TMHMID,Name,Type,Desc")] TMHM tMHM)
        {
            if (ModelState.IsValid)
            {
                db.TMHMs.Add(tMHM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tMHM);
        }

        // GET: TMHM/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMHM tMHM = db.TMHMs.Find(id);
            if (tMHM == null)
            {
                return HttpNotFound();
            }
            return View(tMHM);
        }

        // POST: TMHM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TMHMID,Name,Type,Desc")] TMHM tMHM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tMHM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tMHM);
        }

        // GET: TMHM/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TMHM tMHM = db.TMHMs.Find(id);
            if (tMHM == null)
            {
                return HttpNotFound();
            }
            return View(tMHM);
        }

        // POST: TMHM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TMHM tMHM = db.TMHMs.Find(id);
            db.TMHMs.Remove(tMHM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
