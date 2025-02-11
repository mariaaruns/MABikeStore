using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;

namespace BikeStore.Domain.Models
{

    public partial class Brand
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public string? Logo { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}