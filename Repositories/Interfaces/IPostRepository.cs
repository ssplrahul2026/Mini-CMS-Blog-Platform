using MiniCMS.Models.Entities;

namespace MiniCMS.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post?> GetPostDetailsByIdAsync(int id);
    }
}