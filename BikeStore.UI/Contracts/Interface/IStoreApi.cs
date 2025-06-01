using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.StoreResponse;
using MudBlazor;

namespace BikeStore.UI.Contracts.Interface
{
    public interface IStoreApi
    {
        Task<ApiResponse<PaginationModel<GetStoreResponse>>> GetAllStoreAsync (GetStoreRequest request);
        Task<ApiResponse<GetStoreResponse>> GetStoreDropDown();
        Task<ApiResponse<GetStoreCountResponse>> GetStoreCountAsync();
        Task<ApiResponse<CreateStoreResponse>> CreateStoreAsync(CreateStoreRequest request);
        Task<ApiResponse<UpdateStoreResponse>> UpdateStoreAsync(UpdateStoreRequest request);
        Task<ApiResponse<UpdateStoreResponse>> GetStoreByIdAsync(int id);
    }
}
