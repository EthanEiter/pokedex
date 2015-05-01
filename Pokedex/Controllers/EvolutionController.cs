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
    public class EvolutionController : Controller
    {
        private DexContext db = new DexContext();

        // GET: Evolution
        public ActionResult Index()
        {
            return View(db.Evolutions.ToList());
        }

        // GET: Evolution/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evolution evolution = db.Evolutions.Find(id);
            if (evolution == null)
            {
                return HttpNotFound();
            }
            return View(evolution);
        }

        // GET: Evolution/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evolution/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvolutionID,PKMNFromID,Lvl,Notes")] Evolution evolution)
        {
            if (ModelState.IsValid)
            {
                db.Evolutions.Add(evolution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evolution);
        }

        // GET: Evolution/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evolution evolution = db.Evolutions.Find(id);
            if (evolution == null)
            {
                return HttpNotFound();
            }
            return View(evolution);
        }

        // POST: Evolution/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvolutionID,PKMNFromID,Lvl,Notes")] Evolution evolution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evolution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evolution);
        }

        // GET: Evolution/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evolution evolution = db.Evolutions.Find(id);
            if (evolution == null)
            {
                return HttpNotFound();
            }
            return View(evolution);
        }

        // POST: Evolution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evolution evolution = db.Evolutions.Find(id);
            db.Evolutions.Remove(evolution);
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
