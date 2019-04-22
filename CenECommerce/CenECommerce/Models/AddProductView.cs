namespace CenECommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class AddProductView
    {
        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue,
            ErrorMessage = "You must select a {0}")]
        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}",
            ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Range(0, double.MaxValue,
            ErrorMessage = "You must enter values greater than {1} values in {0}")]
        public double Quantity { get; set; }
    }
}