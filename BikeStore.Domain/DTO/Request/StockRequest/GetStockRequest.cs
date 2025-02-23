using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.Stock
{
    public class GetStockRequest:PaginationInput
    {
        public int StoreId { get; set; }

        public string? ProductName { get; set; }

        public int QuantityLessThan { get; set; }

    }
}
