namespace CenECommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class Tax
    {
        [Key]
        public int TaxId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(50, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 3)]
        [Index("Tax_CompanyId_Description_Index", 2, IsUnique = true)]
        [Display(Name = "Tax")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(0, 1, ErrorMessage = "You must enter a {0} rate between {1} and {2}")]
        [DisplayFormat(DataFormatString = "{0:P2}", 
            ApplyFormatInEditMode = false)]
        public double Rate { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Company")]
        [Index("Tax_CompanyId_Description_Index", 1, IsUnique = true)]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}