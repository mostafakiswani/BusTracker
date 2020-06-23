using Entities;
using Entities.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IUsersRepository
    {
        void Block(Guid id);
        void Delete(Guid id);
        bool IsAuthorized(Guid id);
        bool IsExisit(string email, string phonenumber);
        User Login(LoginDto loginDto);
        User Register(RegisterDto registerDto);
    }
}
