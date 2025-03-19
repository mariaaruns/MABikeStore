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
    public class InvoiceItemsRepository : GenericRepository<InvoiceItems>, IInvoiceItemsRepository
    {
        public InvoiceItemsRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        
        }

        public async Task<bool> AddInvoiceItems(List<InvoiceItems> enttiyList)
        {
            await _dbContext.InvoiceItems.AddRangeAsync(enttiyList);
            return true;
        }
    }

    

}
