using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.Models;
using BikeStore.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        
        }

        public async Task<int> AddNewInvoice(Invoice entity)
        {
           var InsertedEntity =await _dbContext.Invoice.AddAsync(entity);
           return InsertedEntity.Entity.Id;
        }
    }
}
