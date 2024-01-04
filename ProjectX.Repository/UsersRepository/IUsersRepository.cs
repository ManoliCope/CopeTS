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
        public int GetUserID(Guid id);
        public TR_Users Login(string username, string password);
        public UsersResp ModifyUser(UsersReq req, string act, int userid);
        public UsersSearchResp GetUsersList(UsersSearchReq req);
        public TR_Users GetUser(int userid);
        public ResetPass resetPass(ResetPass res);
        public TR_Users GetUserRights(int userid);
        public string getUserPass(int userid);
        public UserProductResp ModifyUsersProduct(UsProReq req);
        public List<TR_UsersProduct> GetUsersProduct(int userid);
        public List<LookUpp> GetUsersChildren(int userid);
        public List<TR_Users> GetListedUserWithChildren(int userid);
        public UserProductResp SaveUploadedLogo(UsProReq req);
        public UserProductResp clearUploadedLogo(int userid);

    }
}
