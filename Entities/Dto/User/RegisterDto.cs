﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto.User
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Password { get; set; }
        public string DeviceToken { get; set; }
    }
}
