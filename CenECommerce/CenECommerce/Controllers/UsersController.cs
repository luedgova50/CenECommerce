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
    public class UsersController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: Users
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);

            var users = 
                db.Users.
                Include(u => u.City).
                Include(u => u.Company).
                Include(u => u.State);

            return View(
                users.
                OrderBy(
                    c => c.StateId).
                    ThenBy(
                    ct => ct.CityId).
                    ThenBy(
                    co => co.CompanyId).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityId = 
                new SelectList(
                     ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity");

            ViewBag.CompanyId = 
                new SelectList(
                     ComboBoxHelpers.
                    GetCompanies(), 
                    "CompanyId", 
                    "NameCompany");

            ViewBag.StateId = 
                new SelectList(
                     ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState");

            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);

                try
                {
                    db.SaveChanges();

                    UsersHelpers.
                        CreateUserASP(user.UserName, "User");

                    if (user.PhotoFile != null)
                    {
                        var folder = "~/Content/Photos";

                        var file = string.Format("{0}.jpg",
                           user.UserID);

                        var response =
                            FilesHelpers.
                            UploadPhoto(
                            user.PhotoFile,
                            folder, file);

                        if (response)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder,
                             file);

                            user.Photo = pic;

                            db.Entry(user).State = 
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

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity", 
                    user.CityId);

            ViewBag.CompanyId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCompanies(), 
                    "CompanyId", 
                    "NameCompany", 
                    user.CompanyId);

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    user.StateId);

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity", 
                    user.CityId);

            ViewBag.CompanyId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCompanies(), 
                    "CompanyId", 
                    "NameCompany", 
                    user.CompanyId);

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    user.StateId);

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.PhotoFile != null)
                {
                    var folder = "~/Content/Photos";

                    var pic = string.Empty;

                    var file = string.Format("{0}.jpg", user.UserID);

                    var response =
                        FilesHelpers.
                        UploadPhoto(
                        user.PhotoFile,
                        folder,
                        file);

                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, user.UserID);

                        user.Photo = pic;
                    }

                }

                var db2 = new CenECommerceContext();

                var currentUser = db2.Users.Find(user.UserID);

                if (currentUser.UserName != null)
                {

                    UsersHelpers.UpdateUserName(currentUser.UserName, user.UserName);

                }

                db2.Dispose();

                db.Entry(user).State =
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
                    user.CityId);

            ViewBag.CompanyId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCompanies(), 
                    "CompanyId", 
                    "NameCompany", 
                    user.CompanyId);

            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    user.StateId);

            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);

            db.Users.Remove(user);

            try
            {
                db.SaveChanges();

                UsersHelpers.
                    DeleteUser(user.UserName);

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

            return View(user);
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
