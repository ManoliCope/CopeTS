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
        public TR_Users GetUser(int userid);
        public ResetPass resetPass(ResetPass res);
        public TR_Users GetUserRights(int userid);
        public string getUserPass(int userid);
        public UsProResp ModifyUsersProduct(UsProReq req);
        public List<TR_UsersProduct> GetUsersProduct(int userid);
        public List<LookUpp> GetUsersChildren(int userid);
        public List<TR_Users> GetListedUserWithChildren(int userid);

    }
}
