using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Repositories.Interfaces;
using MiniCMS.ViewModels;

namespace MiniCMS.Repositories.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            return new DashboardViewModel
            {
                TotalPosts = await _context.Posts.CountAsync(p => !p.IsDeleted),
                TotalCategories = await _context.Categories.CountAsync(),
                TotalTags = await _context.Tags.CountAsync(),
                TotalComments = await _context.Comments.CountAsync(),



                DraftPosts = await _context.Posts.CountAsync(p =>!p.IsPublished &&!p.IsDeleted),

                PublishedPosts = await _context.Posts.CountAsync(p =>p.IsPublished &&!p.IsDeleted)
            };
        }
    }
}