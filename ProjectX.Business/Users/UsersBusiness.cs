using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Users;
using ProjectX.Repository.UsersRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;
namespace ProjectX.Business.Users
{
    public class UsersBusiness : IUsersBusiness
    {
        IUsersRepository _usersRepository;

        public UsersBusiness(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public UsersResp ModifyUser(UsersReq req, string act, int userid)
        {
            UsersResp response = new UsersResp();
            response = _usersRepository.ModifyUser(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Users");
            return response;
           
        }
        public List<TR_Users> GetUsersList(UsersSearchReq req)
        {
            return _usersRepository.GetUsersList(req);
        }
        public UsersResp GetUser(int IdUser)
        {
            TR_Users repores = _usersRepository.GetUser(IdUser);
            UsersResp resp = new UsersResp();
            //resp.id = repores.B_Id;
            //resp.title = repores.B_Title;
            //resp.limit= repores.B_Limit;

            return resp;
        }
    }
}
