using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string DeviceToken { get; set; }
        public bool IsBlocked { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
