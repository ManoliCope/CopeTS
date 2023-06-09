using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Models.User;

namespace ProjectX.Business.User
{
    public interface IUserBusiness
    {
        LoginResp Login(LoginReq Req);
        GetUserResp GetUser(GetUserReq Req);

    }
}
