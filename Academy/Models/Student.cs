using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Models
{
    public class Student 
    {
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string? StudentName { get; set; }
        [Required]
        [EmailAddress]
        public string? StudentEmail { get; set; }

        public string? StudentPhone { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string? Massage { get; set; }
     
        public List<Course>? c { get; set; }

    }
}
