using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.UserRequest
{
    public class AddRoleClaimRequest
    {
        public string RoleName { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

    }
}
