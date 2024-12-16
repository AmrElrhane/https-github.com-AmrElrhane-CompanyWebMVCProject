using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression(
        @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{6,}$",
        ErrorMessage = "Password must be at least 6 characters long, including 1 " +
            "uppercase letter, 1 lowercase letter, 1 number, and 1 special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(Password), ErrorMessage = "Not Matched With Password")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
        public string Email { get; set; }

    }
}
