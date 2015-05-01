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
    public class CaughtController : Controller
    {
        private DexContext db = new DexContext();

        // GET: Caught
        public ActionResult Index()
        {
            var caughts = db.Caughts.Include(c => c.PKMN).Include(c => c.User);
            return View(caughts.ToList());
        }

        // GET: Caught/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caught caught = db.Caughts.Find(id);
            if (caught == null)
            {
                return HttpNotFound();
            }
            return View(caught);
        }

        // GET: Caught/Create
        public ActionResult Create()
        {
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name");
            return View();
        }

        // POST: Caught/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CaughtID,PKMNID,UserID")] Caught caught)
        {
            if (ModelState.IsValid)
            {
                db.Caughts.Add(caught);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", caught.PKMNID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", caught.UserID);
            return View(caught);
        }

        // GET: Caught/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caught caught = db.Caughts.Find(id);
            if (caught == null)
            {
                return HttpNotFound();
            }
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", caught.PKMNID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", caught.UserID);
            return View(caught);
        }

        // POST: Caught/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CaughtID,PKMNID,UserID")] Caught caught)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caught).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PKMNID = new SelectList(db.PKMNs, "PKMNID", "Name", caught.PKMNID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", caught.UserID);
            return View(caught);
        }

        // GET: Caught/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caught caught = db.Caughts.Find(id);
            if (caught == null)
            {
                return HttpNotFound();
            }
            return View(caught);
        }

        // POST: Caught/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Caught caught = db.Caughts.Find(id);
            db.Caughts.Remove(caught);
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
