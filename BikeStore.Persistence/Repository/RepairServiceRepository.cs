using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.RepairServiceRequest;
using BikeStore.Domain.DTO.Response.RepairServiceResposne;
using BikeStore.Domain.Models;
using BikeStore.Persistence.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class RepairServiceRepository : GenericRepository<RepairService>, IRepairServiceRepository
    {
        public RepairServiceRepository(BikeStoresContext dbContext) : base(dbContext)
        {

        }

        public async Task<CreateRepairServiceResponse> AddNewRepairServiceAsync(RepairService entity)
        {
            var newInsertdata= await _dbContext.RepairService.AddAsync(entity);
            var mapEntityToRes= newInsertdata.Entity.Adapt<CreateRepairServiceResponse>();
            return mapEntityToRes;
        }

        public async Task<IQueryable<GetRepairServiceResponse>> GetallRepairServiceAsync(GetRepairServiceRequest request)
        {
            var result = _dbContext.RepairService.AsNoTracking()
                        .Include(x => x.Store)
                        .Join(_dbContext.Users,
                              repair=>repair.ServiceId,
                              user=>user.Id,
                              (repair,user) => new {repair,user})
                        .Select(x=> new GetRepairServiceResponse { 
                            BrandName=x.repair.BrandName,
                            ServiceId=x.repair.ServiceId,
                            AssignTo=x.user.FirstName +" "+ x.user.LastName,
                            BikeNo=x.repair.BikeNo,
                            RepairStatus=x.repair.RepairStatus,
                            StoreId=x.repair.Store.StoreName,
                            EstimatedDate=x.repair.EstimatedDate,
                            CustomerName=x.repair.CustomerName,
                            FromDate=x.repair.FromDate,
                            MobileNo=x.repair.MobileNo
                        });
            await Task.CompletedTask;
            return result;
        }

        public Task TrackRepairStatus()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRepairStatusAsync(UpdateRepairRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
