using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.BrandRequest
{
    public class GetBrandRequest:PaginationInput
    {
        public string? BrandFilter { get; set;}
        public bool IsActive { get; set; }
        public string? SortOption { get; set;}
    }
}
