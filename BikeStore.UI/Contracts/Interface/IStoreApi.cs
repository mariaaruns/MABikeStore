using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.StoreResponse;

namespace BikeStore.UI.Contracts.Interface
{
    public interface IStoreApi
    {
        Task<ApiResponse<PaginationModel<GetStoreResponse>>> GetAllStoreAsync (GetStoreRequest request);
        Task<ApiResponse<GetStoreResponse>> GetStoreDropDown();
             
    }
}
