using System.ComponentModel.DataAnnotations;

namespace Academy.Models.ViewModels
{
    public class CreateTrainerViewModel
    {
        public int TrainerId { get; set; }
        [Display(Name = "Instructor First Name")]
        [Required]
        public string? TrainerFName { get; set; }
        [Display(Name = "Instructor Last Name")]
        [Required]
        public string? TrainerLName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }


        public string? PhoneTraniner { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string? City { get; set; }

        public IFormFile? CVFile { get; set; }
        public IFormFile? TrainerImg { get; set; }
        public string? FB { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }
    }
}
