using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.CategoryResponse
{
    public class GetCategoryCountResponse
    {
        public int ActiveCategoriesCount { get; set; }
        public int InActiveCategoriesCount { get; set; }
    }
}
