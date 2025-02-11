using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO
{
    public class PaginationInput
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
