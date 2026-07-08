using MiniCMS.Models.Entities;

namespace MiniCMS.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);

        Task ApproveAsync(int id);

        Task DeleteAsync(int id);
    }
}