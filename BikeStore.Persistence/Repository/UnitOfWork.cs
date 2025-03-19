using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Persistence.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

        public ILookupRepository LookupRepository  {get; private set; }

        public IStockRepository stockRepository  {get; private set; }

        public IRepairServiceRepository repairServiceRepository { get; private set; }

        public IInvoiceItemsRepository InvoiceItemsRepository { get; private set; }

        public IRepairIssuesRepository RepairIssuesRepository { get; private set; }

        public IInvoiceRepository InvoiceRepository { get; private set; }

        private readonly BikeStoresContext _dbContext;
        private IDbContextTransaction _transaction;
        public UnitOfWork(BikeStoresContext dbcontext) 
        {
            _dbContext = dbcontext;
            BrandRepository = new BrandRepository(_dbContext);
            StoreRepository = new StoreRepository(_dbContext);
            CategoryRepository = new CategoryRepository(_dbContext);
            ProductRepository = new ProductRepository(_dbContext);
            LookupRepository = new LookupRepository(_dbContext);
            repairServiceRepository = new RepairServiceRepository(_dbContext);
            stockRepository = new StockRepository(_dbContext);
            InvoiceRepository = new InvoiceRepository(_dbContext);
            InvoiceItemsRepository = new InvoiceItemsRepository(_dbContext);
            RepairIssuesRepository = new RepairIssuesRepository(_dbContext);
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

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null) {

                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();

            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }
    }
}
