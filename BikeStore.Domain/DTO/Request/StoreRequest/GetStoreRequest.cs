﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.StoreRequest
{
    public class GetStoreRequest:PaginationInput
    {
        public string? StoreNameFilter { get; set; }

        public bool? IsActive { get; set; }

    }
}
