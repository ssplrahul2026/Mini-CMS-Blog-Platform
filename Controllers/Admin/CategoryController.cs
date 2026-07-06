using Microsoft.AspNetCore.Mvc;
using MiniCMS.Models.Entities;
using MiniCMS.Services.Interfaces;

namespace Mini_CMS_Blog_Platform.Controllers.Admin
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View("~/Views/Admin/Category/Index.cshtml", categories);
        }

        // GET
        public IActionResult Create()
        {
            return View("~/Views/Admin/Category/Create.cshtml");
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Category/Create.cshtml", category);
            }

            await _categoryService.AddAsync(category);

            return RedirectToAction("Index");
        }

        // GET
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return View("~/Views/Admin/Category/Edit.cshtml", category);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Category/Edit.cshtml", category);
            }

            await _categoryService.UpdateAsync(category);

            return RedirectToAction("Index");
        }

        // GET
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return View("~/Views/Admin/Category/Delete.cshtml", category);
        }

        // POST
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}