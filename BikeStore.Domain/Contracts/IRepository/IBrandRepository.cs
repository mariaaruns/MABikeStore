using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IBrandRepository:IGenericRepository<Brand>
    {
        
        Task<Brand> UpdateExistingBrandAsync(Brand request);

        Task<List<Brand>> BrandDropdownAsync();

        Task<GetBrandCount> GetBrandCountAsync();

        Task<bool> DeleteBrandAsync(int BrandId);
    }
}
