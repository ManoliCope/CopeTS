using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Users
{
    public interface IUsersBusiness
    {
        public UsersResp ModifyUser(UsersReq req, string act, int userid);
        public List<TR_Users> GetUsersList(UsersSearchReq req);
        public UsersResp GetUser(int IdUser);
        public ResetPass resetPass(ResetPass res);
    }
}
