using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.StoreResponse
{
    public class GetStoreResponse
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

    }
}
