using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Models.Entities;
using MiniCMS.Models.ViewModels;
using MiniCMS.Repositories.Interfaces;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PostService(
            IPostRepository postRepository,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _postRepository = postRepository;
            _context = context;
            _environment = environment;
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


            //cover image path setting
            string imagePath = "";

            if (model.CoverImage != null)
            {
                string uploadFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "coverImages");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string fileName = Guid.NewGuid().ToString() +
                                  Path.GetExtension(model.CoverImage.FileName);

                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.CoverImage.CopyToAsync(stream);
                }

                imagePath = "/uploads/coverImages/" + fileName;
            }


            var post = new Post
            {
                Title = model.Title,
                Slug = model.Slug,
                Body = model.Body,
                CategoryId = model.CategoryId,
                IsPublished = model.IsPublished,
                PublishedDate = model.IsPublished ? DateTime.Now : null,
                CoverImagePath = imagePath
            };

            await _postRepository.AddAsync(post);
            await _postRepository.SaveAsync();

            foreach (var tagId in model.SelectedTags)
            {
                _context.PostTags.Add(new PostTag
                {
                    PostId = post.PostId,
                    TagId = tagId
                });
            }

            await _context.SaveChangesAsync();
        }



        //edit the existing post  with cover pic 

        public async Task UpdatePostAsync(PostViewModel model)
        {
            var post = await _postRepository.GetPostDetailsByIdAsync(model.PostId);

            if (post == null)
            {
                throw new Exception("Post not found.");
            }

            post.Title = model.Title;
            post.Slug = model.Slug;
            post.Body = model.Body;
            post.CategoryId = model.CategoryId;
            post.IsPublished = model.IsPublished;

            if (model.IsPublished && post.PublishedDate == null)
            {
                post.PublishedDate = DateTime.Now;
            }

            
            if (model.CoverImage != null)
            {
                if (!string.IsNullOrEmpty(post.CoverImagePath))
                {
                    var oldImage = Path.Combine(
                        _environment.WebRootPath,
                        post.CoverImagePath.TrimStart('/'));

                    if (File.Exists(oldImage))
                    {
                        File.Delete(oldImage);
                    }
                }

                string uploadFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "coverImages");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string fileName = Guid.NewGuid().ToString() +
                                  Path.GetExtension(model.CoverImage.FileName);

                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.CoverImage.CopyToAsync(stream);
                }

                post.CoverImagePath = "/uploads/coverImages/" + fileName;
            }


            // Update Tags
            _context.PostTags.RemoveRange(post.PostTags);

            foreach (var tagId in model.SelectedTags)
            {
                post.PostTags.Add(new PostTag
                {
                    PostId = post.PostId,
                    TagId = tagId
                });
            }

            _postRepository.Update(post);

            await _postRepository.SaveAsync();
        }




        public async Task DeletePostAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
                return;

            post.IsDeleted = true;

            _postRepository.Update(post);

            await _postRepository.SaveAsync();
        }


        public async Task<List<Post>> SearchAsync(string searchTerm)
        {
            return await _postRepository.SearchAsync(searchTerm);
        }




        public async Task<(List<Post> Posts, int TotalPosts)> GetFilteredPostsAsync(string? searchTerm,int? categoryId,int? tagId,string? sortBy,int page,int pageSize)
        {
            return await _postRepository.GetFilteredPostsAsync(searchTerm,categoryId,tagId,sortBy,page,pageSize);
        }



        //public


        public async Task<List<Post>> GetPublishedPostsAsync()
        {
            return await _postRepository.GetPublishedPostsAsync();
        }


        public async Task<Post?> GetPublishedPostDetailsAsync(int id)
        {
            return await _postRepository.GetPostDetailsByIdAsync(id);
        }
        public async Task<(List<Post> Posts, int TotalPosts)> GetPublishedPostsAsync(string? searchTerm,int? categoryId,int? tagId,string? sortBy,int page,int pageSize)
        {
            return await _postRepository.GetPublishedPostsAsync(
                searchTerm,
                categoryId,
                tagId,
                sortBy,
                page,
                pageSize);
        }



        public async Task<List<Post>> GetDeletedPostsAsync()
        {
            return await _postRepository.GetDeletedPostsAsync();
        }

        public async Task RestoreAsync(int id)
        {
            await _postRepository.RestoreAsync(id);
        }



        public async Task<Post?> GetPublishedPostBySlugAsync(string slug)
        {
            return await _postRepository.GetPublishedPostBySlugAsync(slug);
        }


    }
}