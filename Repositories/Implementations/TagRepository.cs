using MiniCMS.Data;
using MiniCMS.Models.Entities;
using MiniCMS.Repositories.Interfaces;

namespace MiniCMS.Repositories.Implementations
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}