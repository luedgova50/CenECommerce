namespace CenECommerce.Controllers
{
    
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using CenECommerce.Classes;
    using CenECommerce.Models;
    using PagedList;

    [Authorize(Roles = "Admin")]
    public class CompaniesController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: Companies
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);

            var companies = 
                db.Companies.
                Include(
                    c => c.City).
                    Include(
                    c => c.State);

            return View(
                companies.
                OrderBy(
                    c => c.StateId).
                    ThenBy(
                    ct => ct.CityId).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity");

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState");

            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);

                try
                {
                    db.SaveChanges();

                    if (company.LogoFile != null)
                    {
                        var folder = "~/Content/Logos";

                        var file = string.Format("{0}.jpg",
                           company.CompanyId);

                        var response =
                            FilesHelpers.
                            UploadPhoto(
                            company.LogoFile,
                            folder, file);

                        if (response)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder,
                             file);

                            company.Logo = pic;

                            db.Entry(company).State = EntityState.Modified;

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

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity", 
                    company.CityId);

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    company.StateId);

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity", 
                    company.CityId);

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    company.StateId);

            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = 
                    EntityState.Modified;

                if (company.LogoFile != null)
                {
                    var folder = "~/Content/Logos";

                    var pic = string.Empty;

                    var file = string.Format("{0}.jpg", company.CompanyId);

                    var response =
                        FilesHelpers.
                        UploadPhoto(
                        company.LogoFile,
                        folder,
                        file);

                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, company.CompanyId);

                        company.Logo = pic;
                    }

                }

                db.Entry(company).State =
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

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity", 
                    company.CityId);

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "StateId", 
                    "NameState", 
                    company.StateId);

            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var company = db.Companies.Find(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var company = db.Companies.Find(id);

            db.Companies.Remove(company);

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

            return View(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        public JsonResult GetCities(int stateId)
        {
            db.Configuration.
                ProxyCreationEnabled = false;

            var cities =
                db.Cities.Where(
                    c => c.StateId == stateId);

            return Json(cities);
        }
    }
}
