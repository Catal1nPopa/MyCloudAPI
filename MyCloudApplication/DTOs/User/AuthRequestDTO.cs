﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCloudApplication.DTOs.User
{
    public class AuthRequestDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}