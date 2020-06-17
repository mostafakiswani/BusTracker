using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private IRepository<User> repository;
        public UsersRepository(IRepository<User> repository)
        { this.repository = repository; }


        public void Block(Guid id)
        {
            var userFromDb = repository.GetById(id);

            userFromDb.IsBlocked = true;

            repository.Update(userFromDb);

        }





    }
}
