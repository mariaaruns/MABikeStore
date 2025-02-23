using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.UserResponse
{
    public class EditRoleClaimResponse
    {
        public bool IsSuccess { get; set; }

        public string? ErrorDesc { get; set; }

    }
}
