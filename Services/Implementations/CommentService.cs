using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task ApproveAsync(int id)
        {
            await _commentRepository.ApproveAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }


        public async Task<List<Comment>> GetApprovedCommentsAsync(int postId)
        {
            return await _commentRepository.GetApprovedCommentsAsync(postId);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _commentRepository.AddCommentAsync(comment);
        }


    }
}