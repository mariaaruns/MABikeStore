﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.DTO.Response.UserResponse
{
    public class RegisterResponse
    {
        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }
    }
}
