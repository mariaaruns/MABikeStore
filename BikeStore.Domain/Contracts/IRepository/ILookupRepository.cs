using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface ILookupRepository:IGenericRepository<Lookup>
    {
        Task<IQueryable<Lookup>> GetLookUpData(string LookupName);
    }
}
