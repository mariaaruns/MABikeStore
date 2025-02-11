using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.BrandResponse
{
    public class GetBrandCount
    {
        public int ActiveBrandsCount { get; set; }
        public int InActiveBrandsCount { get; set; }
    }
}
