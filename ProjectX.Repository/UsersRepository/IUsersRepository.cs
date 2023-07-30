using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.UsersRepository
{
    public interface IUsersRepository
    {
        public TR_Users Login(string username, string password);
        public UsersResp ModifyUser(UsersReq req, string act, int userid);
        public List<TR_Users> GetUsersList(UsersSearchReq req);
        public TR_Users GetUser(int IdUser);
        public ResetPass resetPass(ResetPass res);

    }
}
