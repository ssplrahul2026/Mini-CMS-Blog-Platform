using MiniCMS.Data;
using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;

namespace MiniCMS.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
    : base(context)
        {
        }
    }
}