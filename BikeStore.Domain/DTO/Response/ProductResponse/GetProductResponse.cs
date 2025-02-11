using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BikeStore.Domain.DTO.Response.ProductResponse
{

        public class GetProductResponse
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public string Brand { get; set; }

            public string Category { get; set; }

            public short ModelYear { get; set; }

            public decimal ListPrice { get; set; }

            public string? Image {get; set;}
        }

}
