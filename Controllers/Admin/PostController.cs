using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniCMS.Models.Entities;
using MiniCMS.Models.ViewModels;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;


       
        public PostController(IPostService postService, ICategoryService categoryService, ITagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }


        public async Task<IActionResult> Index(string? searchTerm,int? categoryId,int? tagId,string? sortBy,int page = 1)
        {
            const int pageSize = 7;
          
            var result = await _postService.GetFilteredPostsAsync(searchTerm,categoryId,tagId,sortBy,page,pageSize);

            // Data
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            ViewBag.TagId = tagId;
            ViewBag.SortBy = sortBy;

            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            // Pagination
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPosts = result.TotalPosts;
            ViewBag.TotalPages = (int)Math.Ceiling(result.TotalPosts / (double)pageSize);
          
            return View("~/Views/Admin/Post/Index.cshtml", result.Posts);
        }






        public async Task<IActionResult> Create()
        {
            var model = new PostViewModel();

            var categories = await _categoryService.GetAllAsync();

            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList();

            var tags = await _tagService.GetAllAsync();

            model.Tags = tags.Select(t => new SelectListItem
            {
                Value = t.TagId.ToString(),
                Text = t.Name
            }).ToList();

            return View("~/Views/Admin/Post/Create.cshtml", model);
        }






        [HttpPost]
      
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();

                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();

                var tags = await _tagService.GetAllAsync();

                model.Tags = tags.Select(t => new SelectListItem
                {
                    Value = t.TagId.ToString(),
                    Text = t.Name
                }).ToList();

                return View("~/Views/Admin/Post/Create.cshtml", model);
            }

            await _postService.CreatePostAsync(model);

            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var model = new PostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Slug = post.Slug,
                Body = post.Body,
                CategoryId = post.CategoryId,
                IsPublished = post.IsPublished,
                ExistingImagePath = post.CoverImagePath,
                SelectedTags = post.PostTags.Select(x => x.TagId).ToList()
            };

            var categories = await _categoryService.GetAllAsync();

            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList();

            
            var tags = await _tagService.GetAllAsync();

            model.Tags = tags.Select(t => new SelectListItem
            {
                Value = t.TagId.ToString(),
                Text = t.Name
            }).ToList();

            return View("~/Views/Admin/Post/Edit.cshtml", model);
        }





        [HttpPost]
     
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();

                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();

                var tags = await _tagService.GetAllAsync();

                model.Tags = tags.Select(t => new SelectListItem
                {
                    Value = t.TagId.ToString(),
                    Text = t.Name
                }).ToList();

                return View("~/Views/Admin/Post/Edit.cshtml", model);
            }

            await _postService.UpdatePostAsync(model);

            return RedirectToAction("Index");
        }





        [HttpPost]
     
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePostAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletedPosts()
        {
            var posts = await _postService.GetDeletedPostsAsync();

            return View("~/Views/Admin/Post/DeletedPosts.cshtml", posts);
        }

        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            await _postService.RestoreAsync(id);

            TempData["Success"] = "Post restored successfully.";

            return RedirectToAction(nameof(DeletedPosts));
        }
    }
}