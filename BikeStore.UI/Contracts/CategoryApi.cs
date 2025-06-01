using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.UI.Contracts.Interface;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BikeStore.UI.Contracts
{
    public class CategoryApi : ICategoryApi
    {

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public CategoryApi(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ApiResponse<GetCategoryResposne>> CreateCategoryAsync(CreateCategoryRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var CreateRequest = await _client.PostAsync($"api/Category/Category", bodyContent);
            var brandContent = await CreateRequest.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<GetCategoryResposne>>(brandContent, _options);
            if (!CreateRequest.IsSuccessStatusCode)
                return result;
            return result;
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
        {
            var content = JsonSerializer.Serialize(id);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var brandRequest = await _client.DeleteFromJsonAsync<ApiResponse<bool>>($"api/Category/Delete/{id}");
            return brandRequest;
        }

        public async Task<ApiResponse<PaginationModel<GetCategoryResposne>>> GetCategoryAsync(GetCategoryRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var brandResult = await _client.PostAsync("api/Category/GetCategory", bodyContent);
            var brandContent = await brandResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<PaginationModel<GetCategoryResposne>>>(brandContent, _options);
            if (!brandResult.IsSuccessStatusCode)
                return result;

            return result;
        }

        public async Task<ApiResponse<GetCategoryResposne>> GetCategoryByIdAsync(int id)
        {
            if (id != 0)
            {
                var result = await _client.GetFromJsonAsync<ApiResponse<GetCategoryResposne>>($"api/Category/{id}");
                return result;
            }
            else
            {
                return new ApiResponse<GetCategoryResposne>();
            }
        }

        public async Task<ApiResponse<GetCategoryCountResponse>> GetCategoryCountAsync()
        {
            var request = await _client.GetFromJsonAsync<ApiResponse<GetCategoryCountResponse>>("api/Category/CategoryCount");
            if (request.StatusCode == System.Net.HttpStatusCode.OK)
                return request;
            else
            {
                return new ApiResponse<GetCategoryCountResponse>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "No Records Found",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<GetCategoryResposne>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var updateRequest = await _client.PutAsync($"api/Category/update/{request.CategoryId}", jsonContent);
            var brandContent = await updateRequest.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<GetCategoryResposne>>(brandContent, _options);
            return result ?? new ApiResponse<GetCategoryResposne>();
        }
    }
}
