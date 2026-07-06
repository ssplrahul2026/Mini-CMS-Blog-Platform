using MiniCMS.Models.Entities;

namespace MiniCMS.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();

        Task<Tag?> GetByIdAsync(int id);

        Task AddAsync(Tag tag);

        Task UpdateAsync(Tag tag);

        Task DeleteAsync(int id);
    }
}