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

    [Authorize(Roles ="User")]
    public class OrdersController : Controller
    {
        private CenECommerceContext db = new CenECommerceContext();

        // GET: AddProducts/Create
        public ActionResult AddProduct()
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            ViewBag.ProductId =
                new SelectList(
                    ComboBoxHelpers.
                    GetProducts(
                        user.CompanyId),
                    "ProductID",
                    "Description");

            return PartialView();
        }

        // POST: AddProduct/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(AddProductView view)
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            if (ModelState.IsValid)
            {
                var orderDetailTmp =
                    db.OrderDetailTmps.
                    Where(odt => odt.UserName ==
                    User.Identity.Name &&
                    odt.ProductID == view.ProductId).
                    FirstOrDefault();

                if(orderDetailTmp == null)
                {
                    var product =
                    db.Products.Find(view.ProductId);

                    orderDetailTmp = new OrderDetailTmp
                    {
                        Description = product.Description,
                        Price = product.Price,
                        ProductID = product.ProductID,
                        Quantity = view.Quantity,
                        TaxRate = product.Tax.Rate,
                        UserName = User.Identity.Name,
                    };

                    db.OrderDetailTmps.
                        Add(orderDetailTmp);
                } 
                else
                {
                    orderDetailTmp.Quantity += view.Quantity;

                    db.Entry(orderDetailTmp).State =
                        EntityState.Modified;
                }

                db.SaveChanges();

                return RedirectToAction("Create");
            }            

            ViewBag.ProductId =
                new SelectList(
                    ComboBoxHelpers.
                    GetProducts(
                        user.CompanyId),
                    "ProductID",
                    "Description");

            return PartialView(view);
        }

        // GET: Orders/DeleteProduct/5
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var orderDetailTmp =
                   db.OrderDetailTmps.
                   Where(odt => odt.UserName ==
                   User.Identity.Name &&
                   odt.ProductID == id).
                   FirstOrDefault();

            if (orderDetailTmp == null)
            {
                return HttpNotFound();
            }

            db.OrderDetailTmps.
                Remove(orderDetailTmp);

            db.SaveChanges();

            return RedirectToAction("Create");
        }        

        // GET: Orders
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

            var orders = 
                db.Orders.
                 Where(c => c.CompanyId == user.CompanyId).
                Include(o => o.Customer).
                Include(o => o.PurchaseStatus);

            return View(
                orders.
                OrderBy(
                    c => c.CompanyId).
                ToPagedList(
                    (int)page, 6));
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            var order = db.Orders.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            ViewBag.CustomerId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCustomers(
                        user.CompanyId), 
                    "CustomerID", 
                    "FullName");

            var newView = new NewOrderView
            {
                DateOrder = DateTime.Now,
                Details =
                    db.OrderDetailTmps.
                    Where(odt => odt.UserName ==
                    User.Identity.Name).
                    ToList(),
            };
           
            return View(newView);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewOrderView view)
        {           

            if (ModelState.IsValid)
            {
                var response = 
                    MovementHelpers.
                    NewOrder(
                        view, 
                        User.Identity.Name);

                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(
                    string.Empty, response.Message);
            }

            var user =
               db.Users.
               Where(us => us.UserName == User.Identity.Name).
               FirstOrDefault();

            ViewBag.CustomerId = 
                new SelectList(
                    ComboBoxHelpers.
                    GetCustomers(
                        user.CompanyId), 
                    "CustomerID", 
                    "FullName", 
                    view.CustomerId);

            view.Details =
                   db.OrderDetailTmps.
                   Where(odt => odt.UserName ==
                   User.Identity.Name).
                   ToList();

            return View(view);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "UserName", order.CustomerId);
            ViewBag.PurchaseStatusId = new SelectList(db.PurchaseStatus, "PurchaseStatusId", "Description", order.PurchaseStatusId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,PurchaseStatusId,DateOrder,Remarks")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "UserName", order.CustomerId);
            ViewBag.PurchaseStatusId = new SelectList(db.PurchaseStatus, "PurchaseStatusId", "Description", order.PurchaseStatusId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
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
    }
}
