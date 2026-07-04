using MiniCMS.Models.Entities;

namespace MiniCMS.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}