﻿using System;
using System.Collections.Generic;

namespace BikeStore.Domain.Models;

public partial class Store
{

    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? ZipCode { get; set; }

    public bool? IsActive { get; set; }
    

    public virtual ICollection<RepairService> RepairServices { get; set; } = new List<RepairService>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
