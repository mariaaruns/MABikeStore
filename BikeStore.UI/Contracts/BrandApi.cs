using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.DTO.Response.UserResponse;
using BikeStore.UI.Authentication;
using BikeStore.UI.Contracts.Interface;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BikeStore.UI.Contracts
{
    public class BrandApi : IBrandApi
    {

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public BrandApi(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }       
        
        public  async Task<ApiResponse<PaginationModel<GetBrandResponse>>> GetAllBrand(GetBrandRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var brandResult = await _client.PostAsync("api/Brand", bodyContent);
            var brandContent = await brandResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<PaginationModel<GetBrandResponse>>>(brandContent, _options);
            if (!brandResult.IsSuccessStatusCode)
                return result;

            return result;
        }

        public Task<GetBrandCount> GetBrandCount()
        {
            throw new NotImplementedException();
        }
    }
}
