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
    public class RepairIssuesRepository : GenericRepository<RepairIssues>, IRepairIssuesRepository
    {
        public RepairIssuesRepository(BikeStoresContext dbContext) : base(dbContext)
        {
    
        }

        public async Task<bool> AddRepairIssues(List<RepairIssues> enttiyList)
        {
            await _dbContext.RepairIssues.AddRangeAsync(enttiyList);
            return true;
        }
    }
}
