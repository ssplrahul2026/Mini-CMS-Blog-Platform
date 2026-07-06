using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniCMS.Models.ViewModels;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;



        public PostController(IPostService postService,ICategoryService categoryService,ITagService tagService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
        }
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();

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
    }
}