﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.RepairRequest
{
    public class GetRepairStatusRequest
    {
        public int ServiceId { get; set; }

        public string StoreName { get; set; }
    }
}
