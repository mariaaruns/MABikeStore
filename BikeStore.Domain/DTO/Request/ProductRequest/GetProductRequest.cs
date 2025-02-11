using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.ProductRequest
{
    public class GetProductRequest:PaginationInput
    {
        public string? ProductFilter { get; set; }
        public int? BrandFilter { get; set; }
        public int? CategoryFilter { get; set; }
    }
}
