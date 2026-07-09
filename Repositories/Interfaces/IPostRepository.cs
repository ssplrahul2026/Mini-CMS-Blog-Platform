using MiniCMS.Models.Entities;

namespace MiniCMS.Repositories.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post?> GetPostDetailsByIdAsync(int id);

        Task<List<Post>> SearchAsync(string searchTerm);


        
        Task<(List<Post> Posts, int TotalPosts)> GetFilteredPostsAsync(string? searchTerm,int? categoryId,int? tagId,string? sortBy, int page,int pageSize);


        //public


        Task<List<Post>> GetPublishedPostsAsync();

        Task<Post?> GetPublishedPostDetailsAsync(int id);

        Task<(List<Post> Posts, int TotalPosts)> GetPublishedPostsAsync(string? searchTerm,int? categoryId,int? tagId,string? sortBy,int page,int pageSize);
        Task<List<Post>> GetDeletedPostsAsync();

        Task RestoreAsync(int id);


        Task<Post?> GetPublishedPostBySlugAsync(string slug);
    }
}