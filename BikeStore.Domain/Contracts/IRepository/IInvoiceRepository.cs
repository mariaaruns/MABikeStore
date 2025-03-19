using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IInvoiceRepository:IGenericRepository<Invoice>
    {
        Task<int> AddNewInvoice(Invoice invoice);

    }
}
