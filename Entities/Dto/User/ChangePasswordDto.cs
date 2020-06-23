using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto.User
{
    public class ChangePasswordDto : BaseDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
