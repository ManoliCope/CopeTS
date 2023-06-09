using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.dbModels;

namespace ProjectX.Repository.UserRepository
{
    public interface IUserRepository
    {
        User Login(string username, string password);
        User GetUser(int idUser);
    }
}
