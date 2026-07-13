using MiniCMS.Models.Entities;

namespace MiniCMS.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);

        Task ApproveAsync(int id);

        Task DeleteAsync(int id);

        Task<List<Comment>> GetApprovedCommentsAsync(int postId);

        Task AddCommentAsync(Comment comment);

    }
}