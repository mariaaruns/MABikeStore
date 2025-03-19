using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    
    public interface IInvoiceItemsRepository:IGenericRepository<InvoiceItems>
    {
        
        Task<bool> AddInvoiceItems(List<InvoiceItems> enttiyList);

    }


    
}
