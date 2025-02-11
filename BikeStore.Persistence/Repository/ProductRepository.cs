using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Persistence.Data;
using BikeStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(BikeStoresContext dbContext) : base(dbContext)
        {

        }

        public async Task<IQueryable<Product>> GetProductListAsync()
        {
            var result =  _dbContext.Products
                .Include(x => x.Brand).Include(x => x.Category).AsNoTracking();
            
            await Task.CompletedTask;
            
            return result;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var result = _dbContext.Products.Update(product);
            await Task.CompletedTask;
            return default;
        }
    }
}
