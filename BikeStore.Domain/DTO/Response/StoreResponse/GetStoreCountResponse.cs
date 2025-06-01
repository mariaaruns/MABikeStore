using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.StoreResponse
{
    public class GetStoreCountResponse
    {

        public int? ActiveStoreCount { get; set; }
        public int? InactiveStoreCount { get; set; }

    }
}
