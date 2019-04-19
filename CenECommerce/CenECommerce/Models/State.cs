namespace CenECommerce.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(30, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 3)]
        [Index("StateId_NameState_Index", 1, IsUnique = true)]
        [Display(Name = "State")]
        public string NameState { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Index("StateId_CodeState_Index", 2, IsUnique = true)]
        [Display(Name = "Code State")]
        [Range(0, 120, ErrorMessage = "The field {0} can take values between {1} and {2}")]
        public int CodeState { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Flag")]
        public string Flag { get; set; }

        [NotMapped]
        public HttpPostedFileBase FlagFile { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Shield")]
        public string Shield { get; set; }

        [NotMapped]
        public HttpPostedFileBase ShieldFile { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}