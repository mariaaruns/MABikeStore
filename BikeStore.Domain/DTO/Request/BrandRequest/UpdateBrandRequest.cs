﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.BrandRequest
{
    public class UpdateBrandRequest
    {
        public int BrandId { get; set; }
        public string? BrandName { get; set; }

        public IFormFile? Logo { get; set; }
        public string? LogoImageBase64String { get; set; }
    }
}
