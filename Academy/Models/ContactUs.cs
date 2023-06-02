using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class ContactUs
    {
        public int ContactUsId { get; set; }
        [Required]
        [Display(Name = "Your Name")]
        public string?  Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Location { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string? Massage { get; set;}
        [Required]
        [Display(Name = "Course Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
       
        public string? Address { get; set; }
    }
}
