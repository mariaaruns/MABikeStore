using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Request.Stock;
using BikeStore.Domain.DTO.Response.Stock;
using BikeStore.Domain.Models;
using BikeStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class StockRepository : GenericRepository<Stock>, IStockRepository
    {
        public StockRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        
        }
        public async Task<IQueryable<Stock>> GetAllStockAsync()
        {
            var result =  _dbContext.Stocks
                          .Include(x => x.Store)
                          .Include(x => x.Product).AsNoTracking();
            await Task.CompletedTask;
            return result;
        }

        public async Task<Stock> UpdateQtyAsync(Stock entity)
        {
             var result = await _dbContext.Stocks.AsNoTracking()
            .Where(x => x.ProductId == entity.ProductId && x.StoreId == entity.StoreId)
            .FirstOrDefaultAsync();

            result.Quantity = entity.Quantity;
            var stockEntity = _dbContext.Stocks.Update(result);
            return stockEntity.Entity;
        }

    }
}
