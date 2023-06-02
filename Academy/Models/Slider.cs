using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Slider
    {
        [Display(Name = "Slider Id")]
        public int SliderId { get; set; }
        [Display(Name = "Slider Title")]
        [Required(ErrorMessage = "Enter Title")]
        public string? SliderTitle { get; set; }
        [Display(Name = "Sub Title")]
        [Required(ErrorMessage = "Enter Sub Title")]
        public string? SliderSubTitle { get; set; }
        [Required(ErrorMessage = "Select Image")]
        public string? SliderImg { get; set; }
    }
}
