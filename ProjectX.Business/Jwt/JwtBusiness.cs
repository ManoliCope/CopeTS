using ProjectX.Entities.AppSettings;
using ProjectX.Entities.JwtModels;
using ProjectX.Repository.UserRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectX.Business.Jwt
{
    public class JwtBusiness : IJwtBusiness
    {
        private readonly TrAppSettings _appSettings;

        public JwtBusiness(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public string generateJwtToken(string UserProfileInJson)
        {
            string result = string.Empty;
            Claim[] claims = new[]{
             new Claim("UserProfile", UserProfileInJson)
            };
            //IDictionary<string, object> claimsdata = new Dictionary<string, object>();
            //claimsdata.Add("UserProfile", UserProfileInJson);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.jwt.Key));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            EncryptingCredentials encryptingCredentials = new EncryptingCredentials(symmetricSecurityKey, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);

            var jwtSecurityToken = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
                _appSettings.jwt.Issuer,
                _appSettings.jwt.Audience,
                new ClaimsIdentity(claims),
                null,
                DateTime.UtcNow.AddMinutes(Convert.ToInt32(_appSettings.jwt.ExpiryInMinutes)),
                null,
                signingCredentials,
                encryptingCredentials
            );

            result = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return result;
        }

        public RefreshToken generateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.Now.AddHours(1),
                    Created = DateTime.Now,
                    CreatedByIp = ipAddress
                };
            }
        }

        public Entities.CookieUser getUserFromToken(string token, ProjectX.Entities.AppSettings.Jwt jwt)
        {
            Entities.CookieUser user = null;
            bool Invalid = false;

            try
            {
                SymmetricSecurityKey signingCredentials = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwt.Key));
                SymmetricSecurityKey encryptingCredentials = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwt.Key));

                //JwtSecurityToken jwt = new JwtSecurityToken(token);

                // Verification
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuers = new string[] { jwt.Issuer },
                    ValidAudiences = new string[] { jwt.Audience },
                    IssuerSigningKey = signingCredentials,
                    TokenDecryptionKey = encryptingCredentials,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    RequireSignedTokens = true
                };

                SecurityToken validatedToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                ClaimsPrincipal claims = handler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                if (DateTime.UtcNow > validatedToken.ValidTo)
                    Invalid = true;

                if (Invalid == false)
                {
                    string userProfile = claims.Claims.FirstOrDefault().Value;
                    user = JsonConvert.DeserializeObject<Entities.CookieUser>(userProfile);
                    // referesh token
                    user.refreshedtoken = generateJwtToken(userProfile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
    }

}
