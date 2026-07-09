using MiniCMS.Models.Entities;

namespace MiniCMS.Models.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public bool IsApproved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int PostId { get; set; }

        // Navigation Property
        public Post? Post { get; set; }
    }
}