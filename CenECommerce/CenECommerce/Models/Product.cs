namespace CenECommerce.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Company")]
        [Index("Product_CompanyId_Description_Index", 1, IsUnique = true)]
        [Index("Product_CompanyId_BarCode_Index", 1, IsUnique = true)]
        public int CompanyId { get; set; }

        [StringLength(30, 
            ErrorMessage = "The field {0} must contain between {2} and {1} characters", 
            MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Product")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Display(Name = "Bar Code")]
        [Index("Product_CompanyId_BarCode_Index", 2, IsUnique = true)]
        [StringLength(13,
            ErrorMessage = "The field {0} must contain maximum {1} characters")]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Tax")]
        public int TaxId { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", 
            ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Range(0, double.MaxValue, 
            ErrorMessage = "You must enter a {0} rate between {1} and {2}")]
        public decimal Price { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual Company Company { get; set; }

        public virtual Category Category { get; set; }

        public virtual Tax Tax { get; set; } 

    }
}