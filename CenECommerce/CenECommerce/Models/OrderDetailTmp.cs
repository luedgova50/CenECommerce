namespace CenECommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class OrderDetailTmp
    {
        [Key]
        public int OrderDetailTmpId { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(256, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 10)]        
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        public int ProductID { get; set; }

        [StringLength(100, ErrorMessage =
            "The field {0} must contain between {2} and {1} characters",
            MinimumLength = 3)]
        [Display(Name = "Product")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(0, 1, ErrorMessage = "You must enter a {0} rate between {1} and {2}")]
        [DisplayFormat(DataFormatString = "{0:P2}",
            ApplyFormatInEditMode = false)]
        [Display(Name = "Tax Rate")]
        public double TaxRate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}",
            ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Range(0, double.MaxValue,
            ErrorMessage = "You must enter a {0} rate between {1} and {2}")]
        public decimal Price { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:N2}",
            ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Range(0, double.MaxValue,
            ErrorMessage = "You must enter a {0} rate between {1} and {2}")]
        public double Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}",
            ApplyFormatInEditMode = false)]
        public decimal Value
        {
            get
            { return Price * (decimal)Quantity; }
        }

        public virtual Product Product { get; set; }
    }
}