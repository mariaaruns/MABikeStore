using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.RepairServiceRequest
{
    public class UpdateRepairRequest:AddNewRepairRequest
    {
        public int RepairId { get; set; }
    
    }
}
