using BikeStore.Domain.DTO.Request.Stock;
using BikeStore.Domain.DTO.Response.Stock;
using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IStockRepository:IGenericRepository<Stock>
    {
        Task<IQueryable<Stock>> GetAllStockAsync();

        Task<Stock> UpdateQtyAsync(Stock entity);

        Task<List<Stock>> AddNewStock(List<Stock> entity);
    }
}
