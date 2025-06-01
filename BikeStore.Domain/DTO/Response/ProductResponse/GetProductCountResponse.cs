using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.ProductResponse
{
    public class GetProductCountResponse
    {
        public int? ActiveProductCount { get; set; }
        public int? InactiveProductCount { get; set; }
    }

}
