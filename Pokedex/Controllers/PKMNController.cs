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
using Pokedex.ViewModels;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace Pokedex.Controllers
{
    public class PKMNController : Controller
    {
        private DexContext db = new DexContext();

        // GET: PKMN
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";
            ViewBag.SubtypeSortParm = sortOrder == "subtype" ? "subtype_desc" : "subtype";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            var poke = from p in db.PKMNs
                       select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                poke = poke.Where(p => (p.PKMNID + p.Name).Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    poke = poke.OrderBy(p => p.Name);
                    break;
                case "name_desc":
                    poke = poke.OrderByDescending(p => p.Name);
                    break;
                case "type":
                    poke = poke.OrderBy(p => p.Type);
                    break;
                case "type_desc":
                    poke = poke.OrderByDescending(p => p.Type);
                    break;
                case "subtype":
                    poke = poke.OrderBy(p => p.Subtype);
                    break;
                case "subtype_desc":
                    poke = poke.OrderByDescending(p => p.Subtype);
                    break;
                default:
                    poke = poke.OrderBy(p => p.PKMNID);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(poke.ToPagedList(pageNumber, pageSize));
        }

        // GET: PKMN/Details/5
        public ActionResult Details(int? id)
        {

            /*IQueryable<EvoGroup> data = from poke in db.Evolutions
                                        where poke.PKMNFromID == (int)id
                                        select new EvoGroup()
                                        {
                                            PKMNNum = poke.PKMNID,
                                            pokeName
                                            Lvl = poke.Lvl,
                                            Notes = poke.Notes
                                        };//*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PKMN pKMN = db.PKMNs.Find(id);
            //pKMN.Evolution.From = db.PKMNs.Find(pKMN.Evolution.PKMNFromID);
            if (pKMN == null)
            {
                return HttpNotFound();
            }
            return View(pKMN);
        }

        // GET: PKMN/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PKMN/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PKMNID,Name,Type,Subtype")] PKMN pKMN)
        {
            if (ModelState.IsValid)
            {
                db.PKMNs.Add(pKMN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pKMN);
        }

        // GET: PKMN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PKMN pKMN = db.PKMNs.Find(id);
            if (pKMN == null)
            {
                return HttpNotFound();
            }
            return View(pKMN);
        }

        // POST: PKMN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PKMNID,Name,Type,Subtype")] PKMN pKMN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pKMN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pKMN);
        }

        // GET: PKMN/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PKMN pKMN = db.PKMNs.Find(id);
            if (pKMN == null)
            {
                return HttpNotFound();
            }
            return View(pKMN);
        }

        // POST: PKMN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PKMN pKMN = db.PKMNs.Find(id);
            db.PKMNs.Remove(pKMN);
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
