using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.BrandRequest
{
    public class CreateBrandRequest
    {
        
        public  string BrandName { get; set; }
        public string? Logo { get; set; }
        public IFormFile? LogoImage { get; set; }
    }
}
