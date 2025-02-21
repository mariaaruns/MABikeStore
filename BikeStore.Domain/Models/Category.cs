using System;
using System.Collections.Generic;

namespace BikeStore.Domain.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;
    public bool? IsActive{ get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
