namespace CenECommerce.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PurchaseStatus
    {
        [Key]
        public int PurchaseStatusId { get; set; }

        [Display(Name = "Purchase Status")]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [StringLength(30, 
            ErrorMessage = "The field {0} must contain between {2} and {1} characters",
            MinimumLength = 3)]
        [Index("PurchaseStatus_Description_Index", IsUnique = true)]
        public string Description { get; set; }


    }
}