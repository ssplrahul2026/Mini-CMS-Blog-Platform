using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // Get All Tags
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _tagRepository.GetAllAsync();
        }

        // Get Tag By Id
        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _tagRepository.GetByIdAsync(id);
        }

        // Create Tag
        public async Task AddAsync(Tag tag)
        {
            tag.Name = tag.Name.Trim();

            await _tagRepository.AddAsync(tag);

            await _tagRepository.SaveAsync();
        }

        // Update Tag
        public async Task UpdateAsync(Tag tag)
        {
            var existingTag = await _tagRepository.GetByIdAsync(tag.TagId);

            if (existingTag == null)
            {
                throw new Exception("Tag not found.");
            }

            existingTag.Name = tag.Name.Trim();

            _tagRepository.Update(existingTag);

            await _tagRepository.SaveAsync();
        }

        // Delete Tag
        public async Task DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);

            if (tag == null)
            {
                throw new Exception("Tag not found.");
            }

            _tagRepository.Delete(tag);

            await _tagRepository.SaveAsync();
        }
    }
}