using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using ProjectX.Entities.AppSettings;
using Microsoft.Extensions.Options;
using Dapper;
using System.Data;
using ProjectX.Entities.dbModels;
using System.Linq;

namespace ProjectX.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public UserRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public User Login(string username, string password)
        {
            User user = new User();

            var param = new DynamicParameters();
            param.Add("@username", username);
            param.Add("@password", password);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_user_login", param, commandType: CommandType.StoredProcedure))
                {
                    user = result.ReadFirstOrDefault<User>();
                    if (user != null)
                    {
                        user.group = result.ReadFirstOrDefault<Group>();
                        if (user.group != null)
                            user.group.pages = result.Read<Page>().ToList();
                    }
                }
            }
            return user;
        }

        public User GetUser(int idUser)
        {
            User user = new User();

            var param = new DynamicParameters();
            param.Add("@IdUser", idUser);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_user_get", param, commandType: CommandType.StoredProcedure))
                {
                    user = result.ReadFirstOrDefault<User>();
                    if (user != null)
                    {
                        user.group = result.ReadFirstOrDefault<Group>();
                        if (user.group != null)
                            user.group.pages = result.Read<Page>().ToList();
                    }
                }
            }
            return user;
        }
    }
}