using Microsoft.EntityFrameworkCore;
using MiniCMS.Data;
using MiniCMS.Repositories.Interfaces;

namespace MiniCMS.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}