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
        public byte[]? ImageBytes { get; set; }
        public string? FileName { get; set; }
    }
}
