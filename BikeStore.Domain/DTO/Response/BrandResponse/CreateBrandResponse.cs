using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.BrandResponse
{
    public class CreateBrandResponse
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string? Logo { get; set; }
    }
}
