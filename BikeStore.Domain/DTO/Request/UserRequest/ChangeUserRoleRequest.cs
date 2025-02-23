using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.UserRequest
{
    public class ChangeUserRoleRequest
    {
        public int UserId { get; set; }
        public string OldUserRole { get; set; }
        public string NewUserRole { get; set; }

    }
}
