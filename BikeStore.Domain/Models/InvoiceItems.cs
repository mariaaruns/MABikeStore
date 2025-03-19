using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Models
{
    public class InvoiceItems
    {
        [Key]
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Quantity { get; set; } = 1;

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        public virtual Invoice Invoices { get; set; }
        
    }
}
