using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto.User
{
    public class LoginDto
    {
        public string Password { get; set; }
        public string Phonenumber { get; set; }
        public string DeviceToken { get; set; }
    }
}
