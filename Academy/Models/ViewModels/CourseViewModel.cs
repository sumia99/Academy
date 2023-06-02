using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Academy.Models.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Course Title")]
        public string? CourseTitle { get; set; }
        [Required]
        [Display(Name = "Course Description")]
        [DataType(DataType.MultilineText)]
        public string? CourseDesc { get; set; }
        [Required]
        [Display(Name = "Course Price")]
        public decimal? Price { get; set; }
        [Required]
        [Display(Name = "Course Image")]
        public IFormFile? Img { get; set; }

        [Display(Name = "Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Start Time")]
        [Required]
        public TimeSpan StartTime { get; set; }
        public string? Duration { get; set; }
        [Required]
        public int? Hours { get; set; }
        public int? Rate { get; set; }
        public string? certificate { get; set; }
        public string? Learn { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
