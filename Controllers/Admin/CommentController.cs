using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCMS.Services.Interfaces;

namespace Mini_CMS_Blog_Platform.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

       
        public async Task<IActionResult> Index()
        {
            var comments = await _commentService.GetAllAsync();
            return View("~/Views/Admin/Comment/Index.cshtml", comments);
        }

        
        public async Task<IActionResult> Approve(int id)
        {
            await _commentService.ApproveAsync(id);
            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            await _commentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}