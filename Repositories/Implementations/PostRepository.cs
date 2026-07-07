using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;

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
                .Where(x => !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<Post?> GetPostDetailsByIdAsync(int id)
        {
            return await _context.Posts
                .Where(x => x.PostId == id && !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync();
        }


    }
}