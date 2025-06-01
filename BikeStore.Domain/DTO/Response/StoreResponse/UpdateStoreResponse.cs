using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.StoreResponse
{
    public class UpdateStoreResponse
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }
    }
}
