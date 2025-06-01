using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IGenericRepository<T> where T:class
    {
        Task<T> CreateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<IQueryable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Expression<Func<T, bool>> condition);
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);



    }
}
