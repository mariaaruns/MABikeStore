using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.StoreResponse;
using BikeStore.UI.Contracts.Interface;

namespace BikeStore.UI.Contracts
{
    public class StoreApi : IStoreApi
    {
        public Task<ApiResponse<PaginationModel<GetStoreResponse>>> GetAllStoreAsync(GetStoreRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<GetStoreResponse>> GetStoreDropDown()
        {
            throw new NotImplementedException();
        }
    }
}
