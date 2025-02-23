using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.Models;
using BikeStore.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class LookupRepository : GenericRepository<Lookup>, ILookupRepository
    {
        public LookupRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Lookup>> GetLookUpData(string LookupName)
        {
            var result =  _dbContext.Lookup.Where(x => x.LookupName.Contains(LookupName) && x.IsActive != false).Select(x=> new Lookup { 
            LookupId=x.LookupId,
            LookupValue=x.LookupValue
            });
            await Task.CompletedTask;
            return result;
        }
    }
}
