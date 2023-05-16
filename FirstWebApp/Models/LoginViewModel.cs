
using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Models
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string Username { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        public bool RememberMe { get; set; }

        
    }
}
