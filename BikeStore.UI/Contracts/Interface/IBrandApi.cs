using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using Microsoft.AspNetCore.Components.Forms;

namespace BikeStore.UI.Contracts.Interface
{
    public interface IBrandApi
    {
        Task<ApiResponse<PaginationModel<GetBrandResponse>>> GetAllBrand(GetBrandRequest request);

        Task<ApiResponse<GetBrandCount>> GetBrandCountAsync();

        Task<ApiResponse<bool>> DeleteBrandAsync(int id);

        Task<ApiResponse<GetBrandResponse>> GetBrandByIdAsync(int id);

        Task<ApiResponse<GetBrandResponse>> UpdateBrandAsync(UpdateBrandRequest request);

        Task<ApiResponse<GetBrandResponse>> CreateBrandAsync(CreateBrandRequest request);
    }
}
