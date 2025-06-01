using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.DTO;
using BikeStore.UI.Contracts.Interface;
using Microsoft.AspNetCore.Components;
using BikeStore.Domain.DTO.Request.BrandRequest;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using BikeStore.UI.AppConstant;

namespace BikeStore.UI.ViewModel
{
    public class BrandViewModel
    {
        private readonly IBrandApi _brandApi;

        public BrandViewModel(IBrandApi brandApi)
        {
            _brandApi= brandApi;
        }


        public string BrandLogoPreview { get; set; }
        public string BrandName { get; set; }
        public int BrandId { get; set; }

        public string storeName { get; set; }
        public string status { get; set; }
        public int ActiveBrandCount { get; set; } = 0;
        public int InactiveBrandCount { get; set; } = 0;
        public bool isOkResponse { get; set; } = false;
        
        public string searchString = string.Empty;

        public PaginationModel<GetBrandResponse>? data;
        
        public UpdateBrandRequest Updaterequest = new();

        public string Base64String = string.Empty;

        public IBrowserFile? _files;
        public byte[]? ImageBytes { get; set; }
       
        public async Task GetBrandCount()
        {
            var result = await _brandApi.GetBrandCountAsync();

            if (result.Data is { })
            {
                ActiveBrandCount = result.Data.ActiveBrandsCount;
                InactiveBrandCount = result.Data.InActiveBrandsCount;

            }
        }

        public async Task<bool> SubmitAsync()
        {
            bool isSuccessfullyUpsert = false;

            if (BrandId != 0)
            {

                var uprequest = new UpdateBrandRequest
                {
                    BrandId = BrandId,
                    BrandName = BrandName,
                    ImageBytes = ImageBytes,
                    FileName = _files?.Name
                };

                isSuccessfullyUpsert = await UpdateBrand(uprequest);
            }
            else
            {
                var addrequest = new CreateBrandRequest
                {
                    BrandName = BrandName,
                    ImageBytes = ImageBytes,
                    FileName = _files?.Name
                };
                isSuccessfullyUpsert = await CreateBrand(addrequest);
            }
            return isSuccessfullyUpsert;
         
        }
   


        public async Task SetImageAsync(IBrowserFile file)
        {
            ClearAttachments();
            _files = file;
            ImageBytes = await _files.ConvertToByteArrayAsync();  // Assuming a helper method to convert the file
            Base64String = Convert.ToBase64String(ImageBytes);
            BrandLogoPreview = $"data:{_files.ContentType};base64,{Base64String}";
        }

        public void ClearAttachments()
        {
            BrandLogoPreview = string.Empty;
        }

        private async Task<bool> UpdateBrand(UpdateBrandRequest request)
        {
            var result = await _brandApi.UpdateBrandAsync(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> CreateBrand(CreateBrandRequest request)
        {
            var result = await _brandApi.CreateBrandAsync(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteBrand(int id) 
        {
            if (id != 0)
            {

                var result = await _brandApi.DeleteBrandAsync(id);
                if (result.Data)
                {
                    var RemoveData = data.Items.FirstOrDefault(x => x.BrandId == id);
                    data.Items.Remove(RemoveData);
                  return true;
                }
            }

            return false;
        }


        public async Task<PaginationModel<GetBrandResponse>> GetAllBrand(GetBrandRequest request)
        {
            isOkResponse = false;

            var result = await _brandApi.GetAllBrand(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                isOkResponse = true;
                return result.Data;
            }
            else
            {
                isOkResponse = false;
                return null;
            }
        }

    }
}
