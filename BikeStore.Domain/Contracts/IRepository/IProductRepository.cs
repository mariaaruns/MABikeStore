using BikeStore.Domain.DTO.Response.ProductResponse;
using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<Product> UpdateProductAsync(Product product);
        
        Task<IQueryable<Product>> GetProductListAsync();

        Task<GetProductCountResponse> GetProductCountAsync();
    }
}
