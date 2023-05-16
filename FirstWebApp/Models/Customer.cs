using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models
{
    public class Customer
    {
        [Key]
        [Required(ErrorMessage = "The Customer ID is required.")]
        [StringLength(maximumLength: 5, ErrorMessage ="CustomerId cannot exceed 5 chars.")]
        [Display(Name = "Customer ID")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Company Name is required.")]
        [MinLength(3, ErrorMessage = "Company Name should be 3 or more letters.")]
        [MaxLength(50, ErrorMessage = "Company Name cannot exceed 50 letters.")]
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Contact Name is required.")]
        [MinLength(3, ErrorMessage = "Contact Name should be 3 or more letters.")]
        [MaxLength(50, ErrorMessage = "Contact Name cannot exceed 50 letters.")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MinLength(3, ErrorMessage = "City should be 3 or more letters.")]
        [MaxLength(50, ErrorMessage = "City cannot exceed 50 letters.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [MinLength(3, ErrorMessage = "Country should be 3 or more letters.")]
        [MaxLength(50, ErrorMessage = "Country cannot exceed 50 letters.")]
        [Display(Name = "Country")]
        public string Country { get; set; }
       
    }
}
