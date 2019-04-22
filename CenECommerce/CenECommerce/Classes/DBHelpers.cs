namespace CenECommerce.Classes
{
   
    using System;
    using System.Linq;
    using CenECommerce.Models;

    public class DBHelpers
    {
        public static Response SaveChanges(CenECommerceContext db)
        {
            try
            {
                db.SaveChanges();
                return new Response { Succeeded = true, };
            }
            catch (Exception ex)
            {
                var response = new Response { Succeeded = false, };
                if (ex.InnerException != null &&
                    ex.InnerException.
                    InnerException != null &&
                    ex.InnerException.
                    InnerException.
                    Message.Contains("_Index"))
                {
                    response.Message = 
                        "There is a record with the same value";
                }
                else if (ex.InnerException != null &&
                    ex.InnerException.
                    InnerException != null &&
                    ex.InnerException.
                    InnerException.
                    Message.Contains("REFERENCE"))
                {
                    response.Message = 
                        "The record can't be delete because it has related records";
                }
                else
                {
                    response.Message = ex.Message;
                }

                return response;
            }
        }

        public static int GetPurchasesStatus(string description, CenECommerceContext db)
        {
            var purchaseStatus =
                db.PurchaseStatus.
                Where(ps => ps.Description == description).
                FirstOrDefault();

            if (purchaseStatus == null)
            {
                purchaseStatus =
                    new PurchaseStatus
                    {
                        Description = description,
                    };

                db.PurchaseStatus.Add(purchaseStatus);

                db.SaveChanges();
            }

            return purchaseStatus.PurchaseStatusId;
        }
    }
}