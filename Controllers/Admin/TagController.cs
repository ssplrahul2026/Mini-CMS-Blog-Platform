using Microsoft.AspNetCore.Mvc;
using MiniCMS.Models.Entities;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Controllers.Admin
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: Tag
        public async Task<IActionResult> Index()
        {
            var tags = await _tagService.GetAllAsync();
            return View("~/Views/Admin/Tag/Index.cshtml", tags);
        }

        // GET: Tag/Create
        public IActionResult Create()
        {
            return View("~/Views/Admin/Tag/Create.cshtml");
        }

        // POST: Tag/Create
        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Tag/Create.cshtml", tag);
            }

            await _tagService.AddAsync(tag);

            return RedirectToAction("Index");
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Tag/Edit.cshtml", tag);
        }

        // POST: Tag/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Tag/Edit.cshtml", tag);
            }

            await _tagService.UpdateAsync(tag);

            return RedirectToAction("Index");
        }

        // GET: Tag/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagService.GetByIdAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Tag/Delete.cshtml", tag);
        }

        // POST: Tag/Delete
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tagService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}