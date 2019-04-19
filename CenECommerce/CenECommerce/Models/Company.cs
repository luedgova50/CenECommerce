namespace CenECommerce.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [StringLength(30, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 4)]
        [Display(Name = "Company")]
        [Index("Company_Index", IsUnique = true)]
        public string NameCompany { get; set; }

        [StringLength(30, ErrorMessage =
            "The field {0} must contain between {2} and {1} characters",
            MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage =
            "The field {0} must contain between {2} and {1} characters",
            MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
            ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Mobile Number")]
        public string Mobile01 { get; set; }

        [Display(Name = "Mobile Number")]
        public string Mobile02 { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Web Page")]
        public string URL { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        [StringLength(30, ErrorMessage =
            "The field {0} must contain between {2} and {1} characters",
            MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Address { get; set; }

        [NotMapped]
        [Display(Name = "Name Contact")]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}",
                FirstName, LastName);
            }
        }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "City")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Display(Name = "Postal Code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }

        public virtual State State { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
    }
}