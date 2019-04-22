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

    [Authorize(Roles = "User")]
    public class CustomersController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: Customers
        public ActionResult Index(int? page = null)
        {
            var user =
                db.Users.
                Where(us => us.UserName == User.Identity.Name).
                FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            page = (page ?? 1);

            var customers = 
                db.Customers.
                Include(c => c.City).
                Where(c => c.CompanyId == user.CompanyId).
                Include(c => c.State);

            return View(
                customers.
                OrderBy(
                    c => c.CompanyId).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(0), 
                    "CityId", 
                    "NameCity");
           
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState");

            var customer =
                new Customer
                {
                    CompanyId = user.CompanyId,
                };

            return View(customer);
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);

                var response = DBHelpers.SaveChanges(db);

                if (response.Succeeded)
                {
                    if (customer.LogoCFile != null)
                    {
                        var folder = "~/Content/Customers";

                        var file = string.Format("{0}.jpg",
                           customer.CustomerID);

                        var response0 =
                            FilesHelpers.
                            UploadPhoto(
                            customer.LogoCFile,
                            folder, file);

                        if (response0)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder,
                             file);

                            customer.LogoC = pic;

                            db.Entry(customer).State =
                                EntityState.Modified;

                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);               
            }

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(customer.StateId), 
                    "CityId", 
                    "NameCity", 
                    customer.CityId);
            
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    customer.StateId);

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(customer.StateId), 
                    "CityId", 
                    "NameCity", 
                    customer.CityId);
            
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    customer.StateId);

            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(customer).State = 
                    EntityState.Modified;

                if (customer.LogoCFile != null)
                {
                    var folder = "~/Content/Customers";

                    var pic = string.Empty;

                    var file = string.Format("{0}.jpg", 
                        customer.CustomerID);

                    var response0 =
                        FilesHelpers.
                        UploadPhoto(
                        customer.LogoCFile,
                        folder,
                        file);

                    if (response0)
                    {
                        pic = string.Format("{0}/{1}", 
                            folder, customer.CustomerID);

                        customer.LogoC = pic;
                    }

                }

                var response = DBHelpers.SaveChanges(db);

                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);
            }

            ViewBag.CityId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCities(customer.StateId), 
                    "CityId", 
                    "NameCity", 
                    customer.CityId);
            
            ViewBag.StateId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetStates(), 
                    "StateId", 
                    "NameState", 
                    customer.StateId);

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = db.Customers.Find(id);

            db.Customers.Remove(customer);

            var response = DBHelpers.SaveChanges(db);

            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                string.Empty, response.Message);

            return View(customer);
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
