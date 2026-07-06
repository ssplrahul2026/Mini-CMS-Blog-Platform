//using MiniCMS.Models.Entities;

//namespace MiniCMS.Models.Entities
//{
//    public class Post
//    {
//        public int PostId { get; set; }

//        public string Title { get; set; } = string.Empty;

//        public string Slug { get; set; } = string.Empty;

//        public string Body { get; set; } = string.Empty;

//        public string? CoverImagePath { get; set; }

//        public bool isPublished { get; set; }

//        public DateTime? PublishedDate { get; set; }

//        public bool IsDeleted { get; set; }

//        // Foreign Key
//        public int CategoryId { get; set; }

//        // Navigation Property
//        public Category? Category { get; set; }

//        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

//        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

//    }
//}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniCMS.Models.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        // Stores only the image path
        public string? CoverImagePath { get; set; }

        // Publish Status
        public bool IsPublished { get; set; }

        // Publish Date
        public DateTime? PublishedDate { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;

        // Foreign Key
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        // Navigation Properties
        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}