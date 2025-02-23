using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Models
{
    public class RepairService
    {
        [Key]
        public int ServiceId {get;set;}
        public string CustomerName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? EstimatedDate { get; set; }
        public string?  Issues { get; set; }
        public int AssignTo { get; set; }
        public string? BikeNo { get; set; }
        public string? BrandName { get; set; }
        public string? MobileNo { get; set; }
        public string? RepairStatus { get;set; }
        
    }
}
