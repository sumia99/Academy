using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Enter User Name")]
        [MinLength(3, ErrorMessage = "Min Length 3 char")]
        [MaxLength(10, ErrorMessage = "Min Length 10 char")]
        public String? UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter User Password")]
        public String? Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public String? Email { get; set; }
        public string? RoleName { get; set; } 
    }
}
