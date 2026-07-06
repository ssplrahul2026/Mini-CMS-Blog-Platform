using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;
using MiniCMS.Services.Interfaces;

namespace MiniCMS.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(Category category)
        {
            //category.Name = category.Name.Trim();

            await _categoryRepository.AddAsync(category);

            await _categoryRepository.SaveAsync();  
        }


        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(category.CategoryId);

            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }

            existingCategory.Name = category.Name;

            _categoryRepository.Update(existingCategory);

            await _categoryRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            _categoryRepository.Delete(category);

            await _categoryRepository.SaveAsync();
        }
    }
}