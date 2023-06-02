using System.ComponentModel.DataAnnotations;

namespace Academy.Models.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Enter Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Enter conform password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "not match")]
        public string? ConfirmPassword { get; set; }

        public string? Phone { get; set; }
    }
}
