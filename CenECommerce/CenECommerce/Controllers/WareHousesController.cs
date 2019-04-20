namespace CenECommerce.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using CenECommerce.Classes;
    using CenECommerce.Models;
    using PagedList;

    [Authorize(Roles = "User")]
    public class WareHousesController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: WareHouses
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);

            var user =
                db.Users.
                Where(us => us.UserName == User.Identity.Name).
                FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var wareHouses = 
                db.WareHouses.
                Include(w => w.City).
                 Where(c => c.CompanyId == user.CompanyId).
                Include(w => w.State);

            return View(
                wareHouses.
                OrderBy(
                    c => c.CompanyId).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: WareHouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var wareHouse = db.WareHouses.Find(id);

            if (wareHouse == null)
            {
                return HttpNotFound();
            }

            return View(wareHouse);
        }

        // GET: WareHouses/Create
        public ActionResult Create()
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

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

            var warehouse =
                new WareHouse
                {
                    CompanyId = user.CompanyId,
                };

            return View(warehouse);
        }

        // POST: WareHouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                db.WareHouses.Add(wareHouse);

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
                    wareHouse.CityId);
            
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    wareHouse.StateId);

            return View(wareHouse);
        }

        // GET: WareHouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var wareHouse = db.WareHouses.Find(id);

            if (wareHouse == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(), 
                    "CityId", 
                    "NameCity", 
                    wareHouse.CityId);
           
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    wareHouse.StateId);

            return View(wareHouse);
        }

        // POST: WareHouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wareHouse).State = 
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
                    wareHouse.CityId);
            
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    wareHouse.StateId);

            return View(wareHouse);
        }

        // GET: WareHouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var wareHouse = db.WareHouses.Find(id);

            if (wareHouse == null)
            {
                return HttpNotFound();
            }

            return View(wareHouse);
        }

        // POST: WareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var wareHouse = db.WareHouses.Find(id);

            db.WareHouses.Remove(wareHouse);

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

            return View(wareHouse);
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
