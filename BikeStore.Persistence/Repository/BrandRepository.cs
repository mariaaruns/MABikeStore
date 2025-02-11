using Azure.Core;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(BikeStoresContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Brand>> BrandDropdownAsync()
        {
            var DropdownList = await _dbContext.Brands.Select(x => new Brand
            {
                BrandId = x.BrandId,
                BrandName = x.BrandName
            }).ToListAsync();

            return DropdownList;
        }

        public async Task<GetBrandCount> GetBrandCount()
        {
            var countData = await _dbContext.Brands
           .GroupBy(x => x.IsActive)
           .Select(g => new GetBrandCount
           {
               ActiveBrandsCount = g.Key ? g.Count() : 0,
               InActiveBrandsCount = !g.Key ? g.Count() : 0
           })
           .ToListAsync();

            return new GetBrandCount
            {
                ActiveBrandsCount = countData.Sum(x => x.ActiveBrandsCount),
                InActiveBrandsCount = countData.Sum(x => x.InActiveBrandsCount)
            };
        }

        public async Task<Brand> UpdateExistingBrandAsync(Brand entity)
        {
            var result = _dbContext.Brands.Update(entity).Entity;
            await Task.CompletedTask;
            return result;
        }


    }
}
