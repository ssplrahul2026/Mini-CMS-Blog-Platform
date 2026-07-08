using MiniCMS.Models.Entities;
using MiniCMS.Models.ViewModels;

namespace MiniCMS.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();

        Task<Post?> GetPostByIdAsync(int id);

        Task CreatePostAsync(PostViewModel model);

        Task UpdatePostAsync(PostViewModel model);

        Task DeletePostAsync(int id);


        Task<List<Post>> SearchAsync(string searchTerm);

        //Task<List<Post>> GetFilteredPostsAsync(string? searchTerm,int? categoryId,int? tagId);


        Task<List<Post>> GetFilteredPostsAsync(string? searchTerm,int? categoryId,int? tagId,string? sortBy);


    }
}