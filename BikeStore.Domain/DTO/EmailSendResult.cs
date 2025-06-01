using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO
{
    public class EmailSendResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

    }

}
