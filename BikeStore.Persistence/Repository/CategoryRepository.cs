using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Persistence.Data;
using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> UpdateCategoryAsync(Category entity)
        {
            var result =  _dbContext.Categories.Update(entity).Entity;
            await Task.CompletedTask;
            return result;
        }
    }
}
