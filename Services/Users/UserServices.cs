using DataAccess.Interfaces;
using Entities;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Users
{
    public class UserServices
    {
        private static IRepository<User> repository;
        public UserServices(IRepository<User> repository)
        { UserServices.repository = repository; }

        public User Register(User user)
        {
            if (user == null)
                return null;

            if (SharedServices.IsNull(user))
                return null;


            return user;
        }
    }
}
