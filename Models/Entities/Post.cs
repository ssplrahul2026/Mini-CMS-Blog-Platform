using MiniCMS.Models.Entities;

namespace MiniCMS.Models.Entities
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public string? CoverImagePath { get; set; }

        public bool Published { get; set; }

        public DateTime? PublishedDate { get; set; }

        public bool IsDeleted { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation Property
        public Category? Category { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}