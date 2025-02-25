﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Request.UserRequest
{
    public class UserRegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public string UserRole { get; set; }
        public string Avatar { get; set; } = "DefaultAvatar";

    }
}
