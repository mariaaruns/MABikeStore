using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.UserResponse
{
    public class AddRoleClaimResponse
    {
        public bool IsSuccess { get; set; }

        public string? ErrorDesc { get; set; }
    }
}
