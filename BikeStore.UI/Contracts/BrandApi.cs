using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.DTO.Response.UserResponse;
using BikeStore.Domain.Models;
using BikeStore.UI.Authentication;
using BikeStore.UI.Contracts.Interface;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
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

       

        public async Task<ApiResponse<bool>> DeleteBrandAsync(int id)
        {
            var brandRequest = await _client.DeleteFromJsonAsync<ApiResponse<bool>>($"api/Brand/DeleteBrand?id={id}");
            return brandRequest;
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

        public async Task<ApiResponse<GetBrandResponse>> GetBrandByIdAsync(int id)
        {
            if (id != 0)
            {
                var result = await _client.GetFromJsonAsync<ApiResponse<GetBrandResponse>>($"api/Brand/{id}");
                return result;
            }
            else
            {
                return new ApiResponse<GetBrandResponse>();
            }
        }
        public async Task<ApiResponse<GetBrandCount>> GetBrandCountAsync()
        {
            var request = await _client.GetFromJsonAsync<ApiResponse<GetBrandCount>>("api/Brand/GetBrandCount");
            if(request.StatusCode==System.Net.HttpStatusCode.OK)
                   return request;
            else
            {
                return new ApiResponse<GetBrandCount>
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "No Records Found",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<GetBrandResponse>> UpdateBrandAsync(UpdateBrandRequest request)
        {
/*
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");*/
            var updateRequest = await _client.PutAsJsonAsync<UpdateBrandRequest>("api/brand/updatebrand", request);
            var brandContent = await updateRequest.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<GetBrandResponse>>(brandContent, _options);
            return result ?? new ApiResponse<GetBrandResponse>();
        }

        public async Task<ApiResponse<GetBrandResponse>> CreateBrandAsync(CreateBrandRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var CreateRequest = await _client.PostAsync($"api/brand/Createbrand", bodyContent);
            var brandContent = await CreateRequest.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<GetBrandResponse>>(brandContent, _options);
            if (!CreateRequest.IsSuccessStatusCode)
                return result;
            return result;
        }
    }
}
