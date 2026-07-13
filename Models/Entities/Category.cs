using MiniCMS.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MiniCMS.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        public string Name { get; set; } = string.Empty;

        

        // Navigation Property
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}