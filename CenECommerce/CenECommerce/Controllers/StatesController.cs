namespace CenECommerce.Controllers
{    
    using System;
    using System.Data.Entity;
    using System.Net;
    using System.Web.Mvc;
    using System.Linq;
    using CenECommerce.Classes;
    using CenECommerce.Models;
    using PagedList;

    [Authorize(Roles = "Admin")]
    public class StatesController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: States
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);

            return View(
                db.States.
                OrderBy(
                    st => st.NameState).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: States/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();
            }

            return View(state);
        }

        // GET: States/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: States/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(State state)
        {
            if (ModelState.IsValid)
            {
                db.States.Add(state);

                var response = DBHelpers.SaveChanges(db);

                if (response.Succeeded)
                {
                    if (state.FlagFile != null)
                    {
                        var folder = "~/Content/Flags";

                        var file = string.Format("{0}.jpg",
                           state.StateId);

                        var response0 =
                            FilesHelpers.
                            UploadPhoto(
                            state.FlagFile,
                            folder, file);

                        if (response0)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder,
                             file);

                            state.Flag = pic;

                            db.Entry(state).State =
                                EntityState.Modified;

                            db.SaveChanges();
                        }
                    }

                    if (state.ShieldFile != null)
                    {
                        var folder1 = "~/Content/Shields";

                        var file1 = string.Format("{0}.jpg",
                           state.StateId);

                        var response1 =
                            FilesHelpers.
                            UploadPhoto(
                            state.ShieldFile,
                            folder1, file1);

                        if (response1)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder1,
                             file1);

                            state.Shield = pic;

                            db.Entry(state).State =
                                EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);                
            }

            return View(state);
        }

        // GET: States/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();
            }

            return View(state);
        }

        // POST: States/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(State state)
        {
            if (ModelState.IsValid)
            {
                db.Entry(state).State = 
                    EntityState.Modified;

                if (state.FlagFile != null)
                {
                    var folder = "~/Content/Flags";

                    var pic = string.Empty;

                    var file = 
                        string.Format("{0}.jpg", 
                        state.StateId);

                    var response0 =
                        FilesHelpers.
                        UploadPhoto(
                        state.FlagFile,
                        folder,
                        file);

                    if (response0)
                    {
                        pic = 
                            string.Format("{0}/{1}", 
                            folder, 
                            state.StateId);

                        state.Flag = pic;
                    }

                }

                if (state.ShieldFile != null)
                {
                    var folder1 = "~/Content/Shields";

                    var pic = string.Empty;

                    var file1 =
                        string.Format("{0}.jpg",
                        state.StateId);

                    var response1 =
                        FilesHelpers.
                        UploadPhoto(
                        state.FlagFile,
                        folder1,
                        file1);

                    if (response1)
                    {
                        pic =
                            string.Format("{0}/{1}",
                            folder1,
                            state.StateId);

                        state.Shield = pic;
                    }

                }

                db.Entry(state).State =
                    EntityState.Modified;

                var response = DBHelpers.SaveChanges(db);

                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);
            }

            return View(state);
        }

        // GET: States/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();
            }

            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var state = db.States.Find(id);

            db.States.Remove(state);

            var response = DBHelpers.SaveChanges(db);

            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                string.Empty, response.Message);

            return View(state);
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
