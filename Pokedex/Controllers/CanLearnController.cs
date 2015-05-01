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
    public class CanLearnController : Controller
    {
        private DexContext db = new DexContext();

        // GET: CanLearn
        public ActionResult Index()
        {
            var canLearns = db.CanLearns.Include(c => c.PKMN);
            return View(canLearns.ToList());
        }

        // GET: CanLearn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CanLearn canLearn = db.CanLearns.Find(id);
            if (canLearn == null)
            {
                return HttpNotFound();
            }
            return View(canLearn);
        }

        // GET: CanLearn/Create
        public ActionResult Create()
        {
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name");
            return View();
        }

        // POST: CanLearn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CanLearnID,PKMNID,TMHMID")] CanLearn canLearn)
        {
            if (ModelState.IsValid)
            {
                db.CanLearns.Add(canLearn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", canLearn.PKMNID);
            return View(canLearn);
        }

        // GET: CanLearn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CanLearn canLearn = db.CanLearns.Find(id);
            if (canLearn == null)
            {
                return HttpNotFound();
            }
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", canLearn.PKMNID);
            return View(canLearn);
        }

        // POST: CanLearn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CanLearnID,PKMNID,TMHMID")] CanLearn canLearn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(canLearn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", canLearn.PKMNID);
            return View(canLearn);
        }

        // GET: CanLearn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CanLearn canLearn = db.CanLearns.Find(id);
            if (canLearn == null)
            {
                return HttpNotFound();
            }
            return View(canLearn);
        }

        // POST: CanLearn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CanLearn canLearn = db.CanLearns.Find(id);
            db.CanLearns.Remove(canLearn);
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
