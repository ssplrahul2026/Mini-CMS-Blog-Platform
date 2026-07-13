using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Models.ViewModels;
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


        public async Task<List<MostCommentedPostVM>> GetMostCommentedPostsAsync()
        {
            return await _context.MostCommentedPosts
                .FromSqlRaw(@"
            SELECT
                P.PostId,
                P.Title,
                COUNT(C.CommentId) AS TotalComments
            FROM Posts P
            LEFT JOIN Comments C
                ON P.PostId = C.PostId
                AND C.IsApproved = 1
                AND C.IsDeleted = 0
            WHERE
                P.IsDeleted = 0
                AND P.IsPublished = 1
            GROUP BY
                P.PostId,
                P.Title
            ORDER BY
                TotalComments DESC
        ")
                .ToListAsync();
        }


    }
}