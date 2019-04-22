namespace CenECommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, 
            ErrorMessage = "You must select a {0}")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, 
            ErrorMessage = "You must select a {0}")]
        [Display(Name = "Purchase Status")]
        public int PurchaseStatusId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", 
            ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime DateOrder { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual PurchaseStatus PurchaseStatus { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}