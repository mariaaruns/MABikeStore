using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.CategoryResponse
{
    public class GetCategoryResposne
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
