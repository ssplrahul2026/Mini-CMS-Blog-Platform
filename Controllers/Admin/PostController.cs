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


        public async Task<IActionResult> Index(
    string? searchTerm,
    int? categoryId,
    int? tagId,
    string? sortBy)
        {
            var posts = await _postService.GetFilteredPostsAsync(
                searchTerm,
                categoryId,
                tagId,
                sortBy);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            ViewBag.TagId = tagId;
            ViewBag.SortBy = sortBy;

            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Tags = await _tagService.GetAllAsync();

            return View("~/Views/Admin/Post/Index.cshtml", posts);
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

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
     
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePostAsync(id);

            return RedirectToAction(nameof(Index));
        }


    }
}