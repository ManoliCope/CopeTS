using ProjectX.Entities.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Repository.UserRepository;
using ProjectX.Entities.Resources;
using ProjectX.Entities;

namespace ProjectX.Business.User
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResp Login(LoginReq Req)
        {
            LoginResp response = new LoginResp();
            response.user = _userRepository.Login(Req.username, Req.password);
            if (response.user != null)
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            else
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidCredentials);
            return response;
        }

        public GetUserResp GetUser(GetUserReq Req)
        {
            GetUserResp response = new GetUserResp();
            response.user = _userRepository.GetUser(Req.idUser);
            if (response.user != null)
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            else
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
            return response;
        }
    }
}
