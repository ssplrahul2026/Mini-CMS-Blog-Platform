

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

     
        public string? CoverImagePath { get; set; }

        
        public bool IsPublished { get; set; }

        public DateTime? PublishedDate { get; set; }

      
        public bool IsDeleted { get; set; } = false;


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

       
        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}