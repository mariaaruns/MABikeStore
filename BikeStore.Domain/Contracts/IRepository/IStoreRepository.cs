using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IStoreRepository :IGenericRepository<Store>
    {
        Task<Store> UpdateStoreAsync(Store entity);

        Task<bool> InactiveAsync(Store entity);

        
    }
}
