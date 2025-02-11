using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.UserResponse
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string? Message { get; set; }

    }
}
