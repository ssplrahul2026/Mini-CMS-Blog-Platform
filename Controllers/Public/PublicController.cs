using Microsoft.AspNetCore.Mvc;
using MiniCMS.Models.Entities;
using MiniCMS.Services.Implementations;
using MiniCMS.Services.Interfaces;
using System.Security.Claims;
namespace Mini_CMS_Blog_Platform.Controllers.Public
{
    public class PublicController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly ICommentService _commentService;

        public PublicController(IPostService postService, ICategoryService categoryService,ITagService tagService, ICommentService commentService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
            _commentService = commentService;
        }


        public async Task<IActionResult> Index(
           string? searchTerm,
           int? categoryId,
           int? tagId,
           string? sortBy,
           int page = 1)
        {
            const int pageSize = 7;

            var result = await _postService.GetPublishedPostsAsync(searchTerm, categoryId,tagId,sortBy,page,pageSize);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            ViewBag.TagId = tagId;
            ViewBag.SortBy = sortBy;

            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPosts = result.TotalPosts;
            ViewBag.TotalPages = (int)Math.Ceiling(result.TotalPosts / (double)pageSize);

            return View("~/Views/Public/Index.cshtml", result.Posts);
        }



        [Route("blog/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var post = await _postService.GetPublishedPostBySlugAsync(slug);

            if (post == null)
                return NotFound();

            var comments = await _commentService.GetApprovedCommentsAsync(post.PostId);

            ViewBag.Comments = comments;

            return View("~/Views/Public/Details.cshtml", post);
        }







        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, string slug, string body)
        {
            if (!User.Identity!.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var authorName = User.Identity!.Name;

            await _commentService.AddCommentAsync(new Comment
            {
                PostId = postId,
                AuthorName = authorName!,
                Body = body,
                IsApproved = false,
                IsDeleted = false,
                CreatedDate = DateTime.Now
            });

            TempData["Success"] = "Comment submitted successfully. It will appear after approval.";

            return RedirectToAction(nameof(Details), new { slug });
        }




    }
}