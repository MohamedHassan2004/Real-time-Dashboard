using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Dashboard.Domain.Interfaces
{

    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    }
}
