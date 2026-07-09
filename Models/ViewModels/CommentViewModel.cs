using System.ComponentModel.DataAnnotations;

namespace MiniCMS.Models.ViewModels
{
    public class CommentViewModel
    {
        public int PostId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}