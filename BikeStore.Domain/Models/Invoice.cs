using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public string Type { get; set; } 

        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public bool IsPaid { get; set; } = false;

        public virtual RepairService RepairServices { get; set; }
        public virtual ICollection<InvoiceItems> InvoiceItems { get; set; }

    }
}
