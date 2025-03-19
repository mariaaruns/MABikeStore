using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.RepairServiceRequest;
using BikeStore.Domain.DTO.Response.RepairServiceResposne;
using BikeStore.Domain.Models;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IRepairServiceRepository:IGenericRepository<RepairService>
    {
        Task TrackRepairStatus();

        Task<IQueryable<GetRepairServiceResponse>> GetallRepairServiceAsync(GetRepairServiceRequest request);

        Task<CreateRepairServiceResponse> AddNewRepairServiceAsync(RepairService entity);

        Task<bool> UpdateRepairStatusAsync(UpdateRepairRequest request);

    }
}
