using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MiniCMS.Models.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        [Required]
        public string Body { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public List<int> SelectedTags { get; set; } = new();

        public bool IsPublished { get; set; }

        public IFormFile? CoverImage { get; set; }

        public string? ExistingImagePath { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();

        public List<SelectListItem> Tags { get; set; } = new();
    }
}