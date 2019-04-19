namespace CenECommerce.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(30, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 3)]
        [Index("NameCity_Index", 2, IsUnique = true)]
        [Display(Name = "City")]
        public string NameCity { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Index("CityId_PostalCode_Index", IsUnique = true)]
        [Display(Name = "Postal Code")]
        [Range(0, 999999, ErrorMessage = "The field {0} can take values between {1} and {2}")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Flag { get; set; }

        [NotMapped]
        public HttpPostedFileBase FlagFile { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Shield { get; set; }

        [NotMapped]
        public HttpPostedFileBase ShieldFile { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "State")]
        [Index("NameCity_Index", 1, IsUnique = true)]
        public int StateId { get; set; }

        public virtual State State { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}