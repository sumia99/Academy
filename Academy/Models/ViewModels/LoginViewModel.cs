using System.ComponentModel.DataAnnotations;

namespace Academy.Models.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Enter Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
