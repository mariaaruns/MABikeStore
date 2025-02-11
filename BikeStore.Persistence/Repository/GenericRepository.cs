using Azure.Identity;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BikeStoresContext _dbContext;

        public GenericRepository(BikeStoresContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
          var CreatedEntity=await  _dbContext.Set<T>().AddAsync(entity);
            return CreatedEntity.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
             _dbContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
             var result=  _dbContext.Set<T>().AsNoTracking().AsQueryable();
             await Task.CompletedTask;
             return result;
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> condition)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(condition).FirstOrDefaultAsync();
        }
    }
}
