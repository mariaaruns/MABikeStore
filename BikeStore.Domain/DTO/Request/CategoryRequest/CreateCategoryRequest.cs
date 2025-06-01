using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.CategoryRequest
{
    public class CreateCategoryRequest
    {
       public string CategoryName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
