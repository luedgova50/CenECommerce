namespace CenECommerce.Classes
{
    using CenECommerce.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class MovementHelpers : IDisposable
    {
        private static CenECommerceContext db =
            new CenECommerceContext();

        public void Dispose()
        {
            db.Dispose();
        }

        public static Response NewOrder(NewOrderView view, string userName)
        {

            using (var transacction = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Users.Where(
                                u => u.UserName == userName).
                                FirstOrDefault();

                    var order = new Order
                    {
                        CompanyId = user.CompanyId,
                        CustomerId = view.CustomerId,
                        DateOrder = view.DateOrder,
                        Remarks = view.Remarks,
                        PurchaseStatusId =
                        DBHelpers.GetPurchasesStatus(
                            "Created Order", db),
                    };

                    db.Orders.Add(order);

                    db.SaveChanges();

                    var details =
                        db.OrderDetailTmps.
                        Where(odt => odt.UserName ==
                        userName).
                        ToList();

                    foreach (var detail in details)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Description = detail.Description,
                            OrderID = order.OrderId,
                            Price = detail.Price,
                            ProductID = detail.ProductID,
                            Quantity = detail.Quantity,
                            TaxRate = detail.TaxRate,
                        };

                        db.OrderDetails.Add(orderDetail);

                        db.OrderDetailTmps.Remove(detail);
                    }

                    db.SaveChanges();

                    transacction.Commit();

                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    transacction.Rollback();

                    return new Response
                    {
                        Message = ex.Message,
                        Succeeded = false,
                    };
                }
            }
        }
    }

}