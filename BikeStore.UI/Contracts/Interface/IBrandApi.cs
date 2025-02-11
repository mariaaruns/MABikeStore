using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;

namespace BikeStore.UI.Contracts.Interface
{
    public interface IBrandApi
    {
        Task<ApiResponse<PaginationModel<GetBrandResponse>>> GetAllBrand(GetBrandRequest request);

        Task<GetBrandCount> GetBrandCount();
    }
}
