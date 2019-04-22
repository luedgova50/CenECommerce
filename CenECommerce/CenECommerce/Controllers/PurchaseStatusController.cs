using System;
using System.Collections.Generic;
using System.Data;
namespace CenECommerce.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using CenECommerce.Classes;
    using CenECommerce.Models;
    using PagedList;

    [Authorize(Roles ="Admin")]
    public class PurchaseStatusController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: PurchaseStatus
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);

            return View(
                db.PurchaseStatus.
                OrderBy(
                    ps => ps.Description).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: PurchaseStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var purchaseStatus = 
                db.PurchaseStatus.Find(id);

            if (purchaseStatus == null)
            {
                return HttpNotFound();
            }

            return View(purchaseStatus);
        }

        // GET: PurchaseStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PurchaseStatus purchaseStatus)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseStatus.
                    Add(purchaseStatus);

                var response = DBHelpers.SaveChanges(db);

                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);
            }

            return View(purchaseStatus);
        }

        // GET: PurchaseStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var purchaseStatus = 
                db.PurchaseStatus.Find(id);

            if (purchaseStatus == null)
            {
                return HttpNotFound();
            }

            return View(purchaseStatus);
        }

        // POST: PurchaseStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PurchaseStatus purchaseStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseStatus).
                    State = EntityState.Modified;

                var response = DBHelpers.SaveChanges(db);

                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);
            }

            return View(purchaseStatus);
        }

        // GET: PurchaseStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var purchaseStatus = 
                db.PurchaseStatus.Find(id);

            if (purchaseStatus == null)
            {
                return HttpNotFound();
            }

            return View(purchaseStatus);
        }

        // POST: PurchaseStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var purchaseStatus = 
                db.PurchaseStatus.Find(id);

            db.PurchaseStatus.
                Remove(purchaseStatus);

            var response = DBHelpers.SaveChanges(db);

            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                string.Empty, response.Message);

            return View(purchaseStatus);
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
