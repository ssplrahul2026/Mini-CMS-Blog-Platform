using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _dashboardService.GetDashboardDataAsync();
            return View(model);
        }


        public async Task<IActionResult> MostCommentedPosts()
        {
            var posts = await _dashboardService.GetMostCommentedPostsAsync();

            return View(posts);
        }


    }
}