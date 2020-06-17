using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IUsersRepository
    {
        void Block(Guid id);
    }
}
