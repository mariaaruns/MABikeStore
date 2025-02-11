using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.CategoryRequest
{
    public class GetCategoryRequest:PaginationInput
    {
        public string? CategoryNameFilter { get; set; } = null!;

    }
}
