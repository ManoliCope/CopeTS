using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Users
{
    public interface IUsersBusiness
    {
        public LoginResp Login(LoginReq Req);
        public GetUserResp GetUserAuth(GetUserReq Req);
        public UserRights GetUserRights(int userid);
        public UsersResp ModifyUser(UsersReq req, string act, int userid);
        public List<TR_Users> GetUsersList(UsersSearchReq req);
        public UsersResp GetUser(int userid);
        public ResetPass resetPass(ResetPass res);
        public string getUserPass(int userid);
        public UsProResp ModifyUsersProduct(UsProReq req);
        public List<TR_UsersProduct> GetUsersProduct(int userid);
        public List<LookUpp> GetUsersChildren(int userid);
        public List<TR_Users> GetListedUserWithChildren(int userid);
        public UsProResp SaveUploadedLogo(UsProReq req);
    }
}
