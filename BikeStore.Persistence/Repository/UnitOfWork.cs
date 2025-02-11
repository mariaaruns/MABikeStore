using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Persistence.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBrandRepository BrandRepository { get; private set; }

        public IStoreRepository StoreRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        private readonly BikeStoresContext _dbContext;

        public UnitOfWork(BikeStoresContext dbcontext) 
        {
            _dbContext = dbcontext;
            BrandRepository = new BrandRepository(_dbContext);
            StoreRepository = new StoreRepository(_dbContext);
            CategoryRepository = new CategoryRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> SaveAsync()
        {
            var result=await _dbContext.SaveChangesAsync();
            return result > 0;
        }

    }
}
