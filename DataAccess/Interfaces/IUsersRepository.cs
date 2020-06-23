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
        void ChangePassword(Guid Id, string password);
        bool IsAuthorized(Guid id);
        bool ValidateUserPassword(Guid id, string password);
        User Login(LoginDto loginDto);
        User Register(RegisterDto registerDto);
        User GetUserByPhonenumber(string phonenumber);
        User GetUserByEmail(string email);

    }
}
