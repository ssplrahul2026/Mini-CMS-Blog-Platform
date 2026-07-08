using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;

namespace MiniCMS.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .Include(c => c.Post)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.CommentId == id);
        }

        public async Task ApproveAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment != null)
            {
                comment.Approved = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}