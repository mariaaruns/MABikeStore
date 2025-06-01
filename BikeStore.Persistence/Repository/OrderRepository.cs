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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        }

    }
}
