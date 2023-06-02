using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Menu
    {
        [Display(Name = "Menu Id")]
        public int MenuId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter Title")]
        [MaxLength(13, ErrorMessage = "Max 13 Char")]
        [MinLength(3, ErrorMessage = "Min 3 Char")]
        public string? MenuTitle { get; set; }
        [Display(Name = "Title Url")]
        [Required(ErrorMessage = "Enter Title Url")]
        public string? MenuUrl { get; set; }
        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

    }
}
