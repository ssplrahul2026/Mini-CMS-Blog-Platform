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




        public async Task<Post?> GetPublishedPostDetailsAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p =>
                    p.PostId == id &&
                    !p.IsDeleted &&
                    p.IsPublished == true);
        }



        public async Task<List<Post>> SearchAsync(string searchTerm)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(searchTerm))
                .ToListAsync();
        }




        public async Task<(List<Post> Posts, int TotalPosts)> GetFilteredPostsAsync(string? searchTerm, int? categoryId, int? tagId, string? sortBy, int page, int pageSize)
        {
            var query = _context.Posts.Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag).AsQueryable();

         
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.Title.Contains(searchTerm) ||
                    p.Slug.Contains(searchTerm));
            }

     
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            if (tagId.HasValue)
            {
                query = query.Where(p =>
                    p.PostTags.Any(pt => pt.TagId == tagId));
            }

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

        
            int totalPosts = await query.CountAsync();



            var posts = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (posts, totalPosts);
        }



        //public


        public async Task<List<Post>> GetPublishedPostsAsync()
        {
            return await _context.Posts
                .Where(p => p.IsPublished == true && !p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();
        }




        public async Task<(List<Post> Posts, int TotalPosts)> GetPublishedPostsAsync(
            string? searchTerm,
            int? categoryId,
            int? tagId,
            string? sortBy,
            int page,
            int pageSize)
        {
            var query = _context.Posts
                .Where(p => !p.IsDeleted && p.IsPublished)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .AsQueryable();

 
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.Title.Contains(searchTerm) ||
                    p.Slug.Contains(searchTerm));
            }

     
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

          
            if (tagId.HasValue)
            {
                query = query.Where(p =>
                    p.PostTags.Any(pt => pt.TagId == tagId.Value));
            }

           
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

           
            int totalPosts = await query.CountAsync();

           
            var posts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (posts, totalPosts);
        }




        public async Task<List<Post>> GetDeletedPostsAsync()
        {
            return await _context.Posts
                .Where(p => p.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();
        }

        public async Task RestoreAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post != null)
            {
                post.IsDeleted = false;

                _context.Posts.Update(post);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Post?> GetPublishedPostBySlugAsync(string slug)
        {
            return await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p =>
                    p.Slug == slug &&
                    p.IsPublished &&
                    !p.IsDeleted);
        }

    }
}