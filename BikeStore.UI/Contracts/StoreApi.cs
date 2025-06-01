using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.DTO.Response.StoreResponse;
using BikeStore.UI.Contracts.Interface;
using MediatR;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace BikeStore.UI.Contracts
{
    public class StoreApi : IStoreApi
    {

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public StoreApi(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<ApiResponse<CreateStoreResponse>> CreateStoreAsync(CreateStoreRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var CreateRequest = await _client.PostAsync($"api/Store/CreateStore", bodyContent);
            var brandContent = await CreateRequest.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<CreateStoreResponse>>(brandContent, _options);
            return result;
        }

        public async Task<ApiResponse<PaginationModel<GetStoreResponse>>> GetAllStoreAsync(GetStoreRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var StoreResult = await _client.PostAsync("api/Store", bodyContent);
            var StoreContent = await StoreResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<PaginationModel<GetStoreResponse>>>(StoreContent, _options);
            return result;
        }

        public async Task<ApiResponse<UpdateStoreResponse>> GetStoreByIdAsync(int id)
        {
            var StoreResponse = await _client.GetFromJsonAsync<ApiResponse<UpdateStoreResponse>>($"api/Store/{id}");
            return StoreResponse;
        }

        public async Task<ApiResponse<GetStoreCountResponse>> GetStoreCountAsync()
        {
            var request = await _client.GetFromJsonAsync<ApiResponse<GetStoreCountResponse>>("api/Store/StoreCount");
            if (request.StatusCode == System.Net.HttpStatusCode.OK)
                return request;
            else
            {
                return new ApiResponse<GetStoreCountResponse>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "No Records Found",
                    Data = null
                };
            }
        }

        public Task<ApiResponse<GetStoreResponse>> GetStoreDropDown()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<UpdateStoreResponse>> UpdateStoreAsync(UpdateStoreRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            var updateRequest = await _client.PutAsync($"api/Store/update/{request.StoreId}", jsonContent);
            var storeContent = await updateRequest.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<UpdateStoreResponse>>(storeContent, _options);
            return result ?? new ApiResponse<UpdateStoreResponse>();
        }
    }
}
