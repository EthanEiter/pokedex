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

namespace Pokedex.Controllers
{
    public class FoundAtController : Controller
    {
        private DexContext db = new DexContext();

        // GET: FoundAt
        public ActionResult Index()
        {
            var foundAts = db.FoundAts.Include(f => f.Map).Include(f => f.PKMN);
            return View(foundAts.ToList());
        }

        // GET: FoundAt/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoundAt foundAt = db.FoundAts.Find(id);
            if (foundAt == null)
            {
                return HttpNotFound();
            }
            return View(foundAt);
        }

        // GET: FoundAt/Create
        public ActionResult Create()
        {
            ViewBag.MapID = new SelectList(db.Maps, "MapID", "Name");
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name");
            return View();
        }

        // POST: FoundAt/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoundAtID,PKMNID,MapID")] FoundAt foundAt)
        {
            if (ModelState.IsValid)
            {
                db.FoundAts.Add(foundAt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MapID = new SelectList(db.Maps, "MapID", "Name", foundAt.MapID);
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", foundAt.PKMNID);
            return View(foundAt);
        }

        // GET: FoundAt/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoundAt foundAt = db.FoundAts.Find(id);
            if (foundAt == null)
            {
                return HttpNotFound();
            }
            ViewBag.MapID = new SelectList(db.Maps, "MapID", "Name", foundAt.MapID);
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", foundAt.PKMNID);
            return View(foundAt);
        }

        // POST: FoundAt/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoundAtID,PKMNID,MapID")] FoundAt foundAt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foundAt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MapID = new SelectList(db.Maps, "MapID", "Name", foundAt.MapID);
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", foundAt.PKMNID);
            return View(foundAt);
        }

        // GET: FoundAt/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoundAt foundAt = db.FoundAts.Find(id);
            if (foundAt == null)
            {
                return HttpNotFound();
            }
            return View(foundAt);
        }

        // POST: FoundAt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoundAt foundAt = db.FoundAts.Find(id);
            db.FoundAts.Remove(foundAt);
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
