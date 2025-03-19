using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Models
{
    public class RepairIssues
    {
        [Key]
        public int Id { get; set; }
        public int RepairServiceId { get; set; }
        public string IssueDescription { get; set; }
        public bool IssueFixed { get; set; }
        public DateTime? FixedDate { get; set; }
        public virtual RepairService RepairServices { get; set; }
    }
}
