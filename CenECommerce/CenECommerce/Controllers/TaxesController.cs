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
    public class TaxesController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: Taxes
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

            var taxes = 
                db.Taxes.
                Where(t => t.CompanyId == user.CompanyId);

            return View(
                taxes.
                OrderBy(
                    c => c.CompanyId).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: Taxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var tax = db.Taxes.Find(id);

            if (tax == null)
            {
                return HttpNotFound();
            }

            return View(tax);
        }

        // GET: Taxes/Create
        public ActionResult Create()
        {
            var user =
                db.Users.
                Where(us => us.UserName == User.Identity.Name).
                FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tax =
                new Tax
                {
                    CompanyId = user.CompanyId,
                };

            return View(tax);
        }

        // POST: Taxes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tax tax)
        {
            if (ModelState.IsValid)
            {
                db.Taxes.Add(tax);

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

            ViewBag.CompanyId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCompanies(), 
                    "CompanyId", 
                    "NameCompany", 
                    tax.CompanyId);

            return View(tax);
        }

        // GET: Taxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var tax = db.Taxes.Find(id);

            if (tax == null)
            {
                return HttpNotFound();
            }
            
            return View(tax);
        }

        // POST: Taxes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tax tax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tax).State = 
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
            
            return View(tax);
        }

        // GET: Taxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var tax = db.Taxes.Find(id);

            if (tax == null)
            {
                return HttpNotFound();
            }

            return View(tax);
        }

        // POST: Taxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tax = db.Taxes.Find(id);

            db.Taxes.Remove(tax);

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

            return View(tax);
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
