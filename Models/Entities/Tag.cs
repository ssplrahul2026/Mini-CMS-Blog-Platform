
namespace MiniCMS.Models.Entities
{
    public class Tag
    {
        public int TagId { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}