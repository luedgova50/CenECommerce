namespace CenECommerce.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(50, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 3)]
        [Index("Category_CompanyId_Description_Index", 2, IsUnique = true)]
        [Display(Name = "Category")]
        public string Description { get; set; }


        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Company")]
        [Index("Category_CompanyId_Description_Index", 1, IsUnique = true)]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}