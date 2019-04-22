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
    public class ProductsController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: Products
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

            var products = 
                db.Products.
                Include(p => p.Category).
                Include(p => p.Tax).
                Where(c => c.CompanyId == user.CompanyId);

            return View(
                products.
                OrderBy(
                    c => c.CompanyId).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            ViewBag.CategoryId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCategories(
                        user.CompanyId), 
                    "CategoryId", 
                    "Description");
            
            ViewBag.TaxId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetTaxes(
                        user.CompanyId), 
                    "TaxId", 
                    "Description");

            var product =
                new Product
                {
                    CompanyId = user.CompanyId,
                };

            return View(product);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Products.Add(product);

                var response = DBHelpers.SaveChanges(db);

                if (response.Succeeded)
                {
                    if (product.ImageFile != null)
                    {
                        var folder = "~/Content/Products";

                        var file = string.Format("{0}.jpg",
                           product.ProductID);

                        var response =
                            FilesHelpers.
                            UploadPhoto(
                            product.ImageFile,
                            folder, file);

                        if (response)
                        {
                            var pic =
                             string.Format("{0}/{1}",
                             folder,
                             file);

                            product.Image = pic;

                            db.Entry(product).State =
                                EntityState.Modified;

                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);                
            }

            ViewBag.CategoryId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCategories(
                        user.CompanyId), 
                    "CategoryId", 
                    "Description", 
                    product.CategoryId);
            
            ViewBag.TaxId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetTaxes(
                        user.CompanyId), 
                    "TaxId", 
                    "Description", 
                    product.TaxId);

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            ViewBag.CategoryId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCategories(
                        user.CompanyId), 
                    "CategoryId", 
                    "Description", 
                    product.CategoryId);

            
            ViewBag.TaxId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetTaxes(
                        user.CompanyId), 
                    "TaxId", 
                    "Description", 
                    product.TaxId);

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(product).State = 
                    EntityState.Modified;

                if (product.ImageFile != null)
                {
                    var folder = "~/Content/Products";

                    var pic = string.Empty;

                    var file = string.Format("{0}.jpg", product.ProductID);

                    var response0 =
                        FilesHelpers.
                        UploadPhoto(
                        product.ImageFile,
                        folder,
                        file);

                    if (response0)
                    {
                        pic = string.Format("{0}/{1}", folder, product.ProductID);

                        product.Image = pic;
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

            ViewBag.CategoryId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCategories(
                        user.CompanyId), 
                    "CategoryId", 
                    "Description", 
                    product.CategoryId);
            
            ViewBag.TaxId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetTaxes(
                        user.CompanyId),                     
                    "TaxId", 
                    "Description", 
                    product.TaxId);

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);

            db.Products.Remove(product);

            var response = DBHelpers.SaveChanges(db);

            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                string.Empty, response.Message);

            return View(product);
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
