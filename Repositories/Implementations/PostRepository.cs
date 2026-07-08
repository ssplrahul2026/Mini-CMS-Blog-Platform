using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;
using System.Globalization;

namespace MiniCMS.Repositories.Implementations
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();
        }

        public async Task<Post?> GetPostDetailsByIdAsync(int id)
        {
            return await _context.Posts
     .Include(p => p.Category)
     .Include(p => p.PostTags)
         .ThenInclude(pt => pt.Tag)
     .FirstOrDefaultAsync(p =>
         p.PostId == id &&
         !p.IsDeleted);
        }


        public async Task<List<Post>> SearchAsync(string searchTerm)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(searchTerm))
                .ToListAsync();
        }


        public async Task<List<Post>> GetFilteredPostsAsync(
       string? searchTerm,
       int? categoryId,
       int? tagId,
       string? sortBy)
        {
            var query = _context.Posts
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.Title.Contains(searchTerm) ||
                    p.Slug.Contains(searchTerm));
            }

            // Category Filter
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            // Tag Filter
            if (tagId.HasValue)
            {
                query = query.Where(p =>
                    p.PostTags.Any(pt => pt.TagId == tagId));
            }

            // Sorting
            switch (sortBy)
            {
                case "oldest":
                    query = query.OrderBy(p => p.PublishedDate);
                    break;

                case "title":
                    query = query.OrderBy(p => p.Title);
                    break;

                case "title_desc":
                    query = query.OrderByDescending(p => p.Title);
                    break;

                default:
                    query = query.OrderByDescending(p => p.PublishedDate);
                    break;
            }

            return await query.ToListAsync();
        }







    }
}