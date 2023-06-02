using System.ComponentModel.DataAnnotations;

namespace Academy.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? UserEmail { get; set; }
        [Required]
        public string? RoleName { get; set; }
    }
}
