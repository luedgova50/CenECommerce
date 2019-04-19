namespace CenECommerce.Controllers
{
    
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using CenECommerce.Classes;
    using CenECommerce.Models;
    using PagedList;

    public class CitiesController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: Cities
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);

            var cities = 
                db.Cities.
                Include(c => c.State);

            return View(
                cities.
                OrderBy(
                    c => c.StateId).
                    ThenBy(
                    ct => ct.NameCity).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var city = db.Cities.Find(id);

            if (city == null)
            {
                return HttpNotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState");

            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);

                try
                {
                    db.SaveChanges();

                    if (city.FlagFile != null)
                    {
                        var folder = "~/Content/Cities/FlagsCities";

                        var file = string.Format("{0}.jpg",
                           city.CityId);

                        var response =
                            FilesHelpers.
                            UploadPhoto(
                            city.FlagFile,
                            folder, file);

                        if (response)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder,
                             file);

                            city.Flag = pic;

                            db.Entry(city).State =
                                EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                    if (city.ShieldFile != null)
                    {
                        var folder1 = "~/Content/Cities/ShieldsCities";

                        var file1 = string.Format("{0}.jpg",
                           city.CityId);

                        var response1 =
                            FilesHelpers.
                            UploadPhoto(
                            city.ShieldFile,
                            folder1, file1);

                        if (response1)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder1,
                             file1);

                            city.Shield = pic;

                            db.Entry(city).State =
                                EntityState.Modified;

                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                        ex.InnerException.
                                        InnerException != null &&
                                        ex.InnerException.
                                        InnerException.Message.
                                        Contains("_Index"))
                    {
                        ModelState.
                            AddModelError(
                            string.Empty,
                            "You Can't Add a New Record, Because There is Already One");
                    }
                    else
                    {
                        ModelState.
                            AddModelError(
                            string.Empty,
                            ex.Message);
                    }
                }
            }

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    city.StateId);

            return View(city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var city = db.Cities.Find(id);

            if (city == null)
            {
                return HttpNotFound();
            }

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    city.StateId);

            return View(city);
        }

        // POST: Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city).State = 
                    EntityState.Modified;

                if (city.FlagFile != null)
                {
                    var folder = "~/Content/Cities/FlagsCities";

                    var pic = string.Empty;

                    var file =
                        string.Format("{0}.jpg",
                        city.CityId);

                    var response =
                        FilesHelpers.
                        UploadPhoto(
                        city.FlagFile,
                        folder,
                        file);

                    if (response)
                    {
                        pic =
                            string.Format("{0}/{1}",
                            folder,
                            city.CityId);

                        city.Flag = pic;
                    }

                }

                if (city.ShieldFile != null)
                {
                    var folder1 = "~/Content/Cities/ShieldsCities";

                    var pic = string.Empty;

                    var file1 =
                        string.Format("{0}.jpg",
                        city.CityId);

                    var response =
                        FilesHelpers.
                        UploadPhoto(
                        city.FlagFile,
                        folder1,
                        file1);

                    if (response)
                    {
                        pic =
                            string.Format("{0}/{1}",
                            folder1,
                            city.CityId);

                        city.Shield = pic;
                    }

                }

                db.Entry(city).State =
                    EntityState.Modified;

                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null &&
                                        ex.InnerException.
                                        InnerException != null &&
                                        ex.InnerException.
                                        InnerException.Message.
                                        Contains("_Index"))
                    {
                        ModelState.
                            AddModelError(
                            string.Empty,
                            "You Can't Add a New Record, Because There is Already One");
                    }
                    else
                    {
                        ModelState.
                            AddModelError(
                            string.Empty,
                            ex.Message);
                    }
                }
            }

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    city.StateId);

            return View(city);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var city = db.Cities.Find(id);

            if (city == null)
            {
                return HttpNotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var city = db.Cities.Find(id);

            db.Cities.Remove(city);

            try
            {
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.
                    InnerException != null &&
                    ex.InnerException.
                    InnerException.Message.
                    Contains("REFERENCE"))
                {
                    ModelState.
                        AddModelError(
                        string.Empty,
                        "The Selected Record can't be Deleted, "
                        + " Because it Already Contains Related Records");
                }
                else
                {
                    ModelState.
                        AddModelError(
                        string.Empty,
                        ex.Message);
                }
            }

            return View(city);
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
