using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;

namespace BikeStore.UI.Contracts.Interface
{
    public interface ICategoryApi
    {
        Task<ApiResponse<PaginationModel<GetCategoryResposne>>> GetCategoryAsync(GetCategoryRequest request);
        


    }
}
