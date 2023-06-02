using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
        [Required]
        [Display(Name = "Category Image")]
        public string? Image { get; set; }

       
    }
}
