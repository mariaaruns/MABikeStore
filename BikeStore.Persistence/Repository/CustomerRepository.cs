﻿using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.Models;
using BikeStore.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BikeStoresContext dbContext) : base(dbContext)
        {
        }
    }
}
