using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Quic;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.Stock
{
    public class UpsertStockRequest
    {
        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }
    }
}
