using MiniCMS.Models.Entities;

namespace MiniCMS.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post?> GetPostDetailsByIdAsync(int id);

        Task<List<Post>> SearchAsync(string searchTerm);


        //Task<List<Post>> GetFilteredPostsAsync(string? searchTerm,int? categoryId,int? tagId);

        Task<List<Post>> GetFilteredPostsAsync(string? searchTerm,int? categoryId,int? tagId,string? sortBy);


    }
}