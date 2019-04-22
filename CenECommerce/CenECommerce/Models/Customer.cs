namespace CenECommerce.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        [Range(1, double.MaxValue, ErrorMessage = "You must select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(256, ErrorMessage =
            "The field {0} can contain maximun {1} and minimum {2} characters",
            MinimumLength = 10)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
            ErrorMessage = "Email is not valid.")]
        [Display(Name = "E-Mail")]
        [Index("Customer_5UserName_Index", IsUnique = true)]
        public string UserName { get; set; }

        [StringLength(30, 
            ErrorMessage = "The field {0} must contain between {2} and {1} characters", 
            MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(30, 
            ErrorMessage = "The field {0} must contain between {2} and {1} characters", 
            MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

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
        [Display(Name = "Image")]
        public string LogoC { get; set; }

        [NotMapped]
        [Display(Name = "Logo")]
        public HttpPostedFileBase LogoCFile { get; set; }

        [StringLength(30, ErrorMessage =
            "The field {0} must contain between {2} and {1} characters",
            MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Address { get; set; }

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

        public virtual Company Company { get; set; }

        public virtual ICollection<Order> Orders { get; set; }


    }
}