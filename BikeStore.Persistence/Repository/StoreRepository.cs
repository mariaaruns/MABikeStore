using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Persistence.Data;
using BikeStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        public StoreRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> InactiveAsync(Store entity)
        {
            entity.IsActive = !entity.IsActive;
            _dbContext.Stores.Update(entity);
            await Task.CompletedTask;
            return true;
        }

        public async Task<Store> UpdateStoreAsync(Store entity)
        {
            var result = _dbContext.Stores.Update(entity);
            await Task.CompletedTask;
            return result.Entity;
        }
    }
}
