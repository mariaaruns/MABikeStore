﻿using BikeStore.Domain.Contracts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts
{

    public interface IUnitOfWork
    {
        IBrandRepository BrandRepository { get; }
        IStoreRepository StoreRepository { get; }
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ILookupRepository LookupRepository { get; }
        IStockRepository stockRepository { get; }
        IRepairServiceRepository repairServiceRepository { get; }
        Task<bool> SaveAsync();
    }


}
