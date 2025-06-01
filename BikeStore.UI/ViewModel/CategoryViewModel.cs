using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.DTO;
using BikeStore.UI.Contracts.Interface;
using BikeStore.UI.Contracts;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.UI.ViewModel
{
    public class CategoryViewModel
    {
        private readonly ICategoryApi _categoryApi;
        public CategoryViewModel(ICategoryApi categoryApi)
        {
            _categoryApi = categoryApi;
        }

        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = null!;

        public PaginationModel<GetCategoryResposne> data;
        public int ActiveCategoryCount { get; set; } = 0;
        public int InactiveCategoryCount { get; set; } = 0;
        public bool isOkResponse { get; set; } = false;
        public string? searchString = null;
        public string status { get; set; }
        public async Task GetCategoryCount() {
            var result = await _categoryApi.GetCategoryCountAsync();
            if (result.Data is { })
            {
                ActiveCategoryCount = result.Data.ActiveCategoriesCount;
                InactiveCategoryCount = result.Data.InActiveCategoriesCount;
                
            }
        }


        public async Task<PaginationModel<GetCategoryResposne>> GetAllCategory(GetCategoryRequest request)
        {
            isOkResponse = false;

            var result = await _categoryApi.GetCategoryAsync(request);

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
        public async Task<bool> SubmitAsync()
        {
            bool isSuccessfullyUpsert = false;

            if (CategoryId != 0)
            {
                var request = new UpdateCategoryRequest
                {
                    CategoryId = CategoryId,
                    CategoryName = CategoryName

                };
                isSuccessfullyUpsert = await UpdateCategory(request);
            }
            else
            {
                var request = new CreateCategoryRequest
                {
                    CategoryName = CategoryName,

                };
                isSuccessfullyUpsert = await CreateCategory(request);
            }
            return isSuccessfullyUpsert;

           

        }
        public async Task<bool> DeleteCategory(int id)
        {
            if (id != 0)
            {

                var result = await _categoryApi.DeleteCategoryAsync(id);
                if (result.Data)
                {
                    var RemoveData = data.Items.FirstOrDefault(x => x.CategoryId == id);
                    data.Items.Remove(RemoveData);
                    return true;
                }
            }
            return false;
        }


        private async Task<bool> CreateCategory(CreateCategoryRequest request)
        {
            var result = await _categoryApi.CreateCategoryAsync(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> UpdateCategory(UpdateCategoryRequest request)
        {
            var result = await _categoryApi.UpdateCategoryAsync(request);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            return false;
        }
        protected async Task GetCategoryDetails(int id)
        {
            var categopry = await _categoryApi.GetCategoryByIdAsync(id);
            if (categopry.StatusCode == System.Net.HttpStatusCode.OK && categopry.Data != null)
            {
                CategoryName = categopry.Data.CategoryName;
            }
        }

    }
}
