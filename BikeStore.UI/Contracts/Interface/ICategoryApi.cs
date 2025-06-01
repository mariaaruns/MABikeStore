using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.DTO.Response.CategoryResponse;

namespace BikeStore.UI.Contracts.Interface
{
    public interface ICategoryApi
    {
        Task<ApiResponse<PaginationModel<GetCategoryResposne>>> GetCategoryAsync(GetCategoryRequest request);

        Task<ApiResponse<GetCategoryCountResponse>> GetCategoryCountAsync();

        Task<ApiResponse<bool>> DeleteCategoryAsync(int id);

        Task<ApiResponse<GetCategoryResposne>> GetCategoryByIdAsync(int id);

        Task<ApiResponse<GetCategoryResposne>> UpdateCategoryAsync(UpdateCategoryRequest request);

        Task<ApiResponse<GetCategoryResposne>> CreateCategoryAsync(CreateCategoryRequest request);


    }
}
