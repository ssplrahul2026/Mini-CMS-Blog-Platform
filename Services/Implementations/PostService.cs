using MiniCMS.Models.Entities;
using MiniCMS.Models.ViewModels;
using MiniCMS.Repositories.Interfaces;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _postRepository.GetPostDetailsByIdAsync(id);
        }

        public async Task CreatePostAsync(PostViewModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Slug = model.Slug,
                Body = model.Body,
                CategoryId = model.CategoryId,
                IsPublished = model.IsPublished,
                PublishedDate = model.IsPublished ? DateTime.Now : null,
                CoverImagePath = ""
            };

            await _postRepository.AddAsync(post);

            await _postRepository.SaveAsync();
        }

        public Task UpdatePostAsync(PostViewModel model)
        {
            return Task.CompletedTask;
        }

        public Task DeletePostAsync(int id)
        {
            return Task.CompletedTask;
        }
    }
}