using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.RepairServiceRequest
{
    public class GetRepairServiceRequest:PaginationInput
    {
        public string? CustomerName { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? EstimatedDate { get; set; }

        public int? AssignTo { get; set; }

        public string? BikeNo { get; set; }

        public string? RepairStatus { get; set; }

        public int StoreId { get; set; }

    }
}
