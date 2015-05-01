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
    public class UserController : Controller
    {
        private DexContext db = new DexContext();

        // GET: User
        public ActionResult Index(string searchString, int? id, int? pkmnid)
        {
            var viewModel = new UserIndexData();
            viewModel.Users = db.Users
                .OrderBy(i => i.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                id = null;
                pkmnid = null;
                viewModel.Users = viewModel.Users.Where(u => u.Name.Contains(searchString));
            }

            if (id != null)
            {
                viewModel.userName = db.Users.Where(i => i.UserID == id.Value).Single().Name;
                ViewBag.UserID = id.Value;
                viewModel.Caughts = viewModel.Users.Where(
                    i => i.UserID == id.Value)
                    .Single().Caughts.OrderBy(p => p.PKMNID);
            }

            if (pkmnid != null)
            {
                viewModel.pokeName = db.PKMNs.Where(i => i.PKMNID == pkmnid.Value).Single().Name;
                ViewBag.PKMNID = pkmnid.Value;
                viewModel.CanLearns = db.PKMNs.Where(
                    x => x.PKMNID == pkmnid.Value)
                    .Single().CanLearns.OrderBy(t => t.TMHMID);
            }

            return View(viewModel);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Name")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(user);
        }

        // GET: User/Edit/5
        // All this method does is display the information on the edit page.
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users
                .Include(i => i.Caughts)
                .Where(i => i.UserID == id)
                .Single();
            if (user == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedPKMNData(user);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string[] selectedPKMN)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userToUpdate = db.Users
                .Include(i => i.Caughts)
                .Where(i => i.UserID == id)
                .Single();

            if (TryUpdateModel(userToUpdate, "",
               new string[] { "Name" }))
            {
                try
                {
                    db.Entry(userToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    UpdateUserPKMN(selectedPKMN, userToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedPKMNData(userToUpdate);
            return View(userToUpdate);
        
        }//*/

        //This is where the update userPKMN method goes
        private void UpdateUserPKMN(string[] selectedPKMN, User user)
        {
            if(selectedPKMN == null)
            {
                selectedPKMN = new string[0];
            }

            var selectedPMKNHS = new HashSet<string>(selectedPKMN);
            var userPKMN = new HashSet<int>(user.Caughts.Select(c => c.PKMNID));
            foreach (var pkmn in db.PKMNs)
            {
                if(selectedPMKNHS.Contains(pkmn.PKMNID.ToString()))
                {
                    if(!userPKMN.Contains(pkmn.PKMNID))
                    {
                        Caught c = new Caught();
                        c.PKMNID = pkmn.PKMNID;
                        c.UserID = user.UserID;
                        db.Caughts.Add(c);
                    }
                }
                else
                {
                    if(userPKMN.Contains(pkmn.PKMNID))
                    {
                        Caught c = db.Caughts
                            .Where(i => i.UserID == user.UserID)
                            .Where(i => i.PKMNID == pkmn.PKMNID)
                            .Single();
                        db.Caughts.Remove(c);
                    }
                }//*/
            }
        }

        // This method was added and changed to fit our project
        private void PopulateAssignedPKMNData(User user)
        {
            var allPKMN = db.PKMNs;
            var userPKMN = new HashSet<int>(user.Caughts.Select(c => c.PKMNID));
            var viewModel = new List<AssignedPKMNData>();
            foreach (var pkmn in allPKMN)
            {
                viewModel.Add(new AssignedPKMNData
                {
                    PKMNID = pkmn.PKMNID,
                    Name = pkmn.Name,
                    Assigned = userPKMN.Contains(pkmn.PKMNID)

                });
            }
            ViewBag.PKMNs = viewModel;
        }
        // GET: User/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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

        public ActionResult Percentage(string searchString)
        {
            double total = db.PKMNs.Count();
            IQueryable<TrainerPercentageGroup> data;

            if (!String.IsNullOrEmpty(searchString))
            {
                data = from user in db.Caughts
                    where (user.User.Name.Contains(searchString))
                    group user by user.User.Name into nameGroup
                    select new TrainerPercentageGroup()
                    {
                        UserName = nameGroup.Key,
                        percentage = (double)nameGroup.Count() / total * 100
                    };
            }
            else
            {
                data = from user in db.Caughts
                    group user by user.User.Name into nameGroup
                    select new TrainerPercentageGroup()
                    {
                        UserName = nameGroup.Key,
                        percentage = (double)nameGroup.Count() / total * 100
                    };
            }
            return View(data.ToList());
        }
    }
}
