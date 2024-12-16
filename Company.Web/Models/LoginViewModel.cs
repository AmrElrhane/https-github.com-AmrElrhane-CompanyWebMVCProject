using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "This Is Wrong Email Format")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
       
        public string Password { get; set; }

        public bool RememberMe { get; set; }    
    }
}
