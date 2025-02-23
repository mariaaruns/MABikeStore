using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.Stock
{
    public class GetStockResponse
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int? Quantity { get; set; }
    }
}
