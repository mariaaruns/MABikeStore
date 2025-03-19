using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.RepairServiceRequest
{
    public class AddNewRepairRequest
    {
        public string CustomerName { get; set; }

        public string BikeNo { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime EstimatedDate { get; set; }

        public int AssignTo { get; set; }

        public string BrandName { get; set; }

        public string MobileNo { get; set; }

        public string RepairStatus { get; set; }

        public int StoreId { get; set; }

        public decimal TotalAmount { get; set; }

        public List<SpareParts> SpareParts { get; set; }

        public List<IssueList> IssueList { get; set; }

    }


    public class SpareParts 
    {
        public string Description { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

    }

    public class IssueList 
    { 
        public string IssueDescription { get; set; }
    }

}
