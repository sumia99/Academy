using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Academy.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
        [Required]
        [Display(Name = "Category Image")]
        public IFormFile ? Image { get; set; }
    }
}
