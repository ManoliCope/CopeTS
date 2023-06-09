using ProjectX.Entities.JwtModels;
using ProjectX.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Jwt
{
    public interface IJwtBusiness
    {
        string generateJwtToken(string UserProfileInJson);
        RefreshToken generateRefreshToken(string ipAddress);
        Entities.CookieUser getUserFromToken(string token, Entities.AppSettings.Jwt jwt);
    }
}
