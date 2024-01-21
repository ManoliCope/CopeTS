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
        public int GetUserID(Guid id);
        public LoginResp Login(LoginReq Req);
        public GetUserResp GetUserAuth(GetUserReq Req);
        public UserRights GetUserRights(int userid);
        public UsersResp ModifyUser(UsersReq req, string act, int userid);
        public UsersSearchResp GetUsersList(UsersSearchReq req);
        public UsersResp GetUser(int userid);
        public ResetPass resetPass(ResetPass res);
        public string getUserPass(int userid);
        public UserProductResp ModifyUsersProduct(UsProReq req);
        public List<TR_UsersProduct> GetUsersProduct(int userid);
        public List<LookUpp> GetUsersChildren(int userid);
        public List<TR_Users> GetListedUserWithChildren(int userid);
        public UserProductResp SaveUploadedLogo(UsProReq req);
        public UserProductResp clearUploadedLogo(int userid);
        public void CopyParentAttachments(int parentId, int childId, string uploadsDirectory);
    }
}
