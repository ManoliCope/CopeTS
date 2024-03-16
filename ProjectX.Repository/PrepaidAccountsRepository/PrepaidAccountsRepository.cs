using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using ProjectX.Entities.Models.PrepaidAccounts;

namespace ProjectX.Repository.PrepaidAccountsRepository
{
    public class PrepaidAccountsRepository : IPrepaidAccountsRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public PrepaidAccountsRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public LoadDataModel GetAvailableUsers(int userid)
        {
            var resp = new LoadDataModel();
            var param = new DynamicParameters();

            param.Add("@userid", userid);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_PA_Get_Users", param, commandType: CommandType.StoredProcedure))
                {
                    resp.users = result.Read<LookUpp>().ToList();

                }
            }

            return resp;
        }
        public PreAccSearchResp GetUserBalance(int userid)
        {
            //int statusCode = 0;

            var resp = new PreAccSearchResp();
            var param = new DynamicParameters();

            param.Add("@userid", userid);
            //param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_PA_Get_User_Balance", param, commandType: CommandType.StoredProcedure))
                {
                    resp.preAccTransactions = result.Read<TR_PrepaidAccountsTransactions>().ToList();
                    resp.PA_Amount = result.Read<float>().FirstOrDefault();
                    //statusCode = param.Get<int>("@Status");

                }
                //resp.statusCode.code=statusCode;
            }

            return resp;
        }
        public PreAccResp EditBalance(int createdBy, int action, float amount, int userid)
        {
            var resp = new PreAccResp();
            var param = new DynamicParameters();
            int statusCode = 0;
            int idOut = 0;
            string message = "";
            param.Add("@createdBy", createdBy);
            param.Add("@userid", userid);
            param.Add("@action", action);
            param.Add("@amount", amount);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Message", message, dbType: DbType.String, direction: ParameterDirection.InputOutput);

           
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                 {
                        _db.Execute("TR_PA_Edit_User_Balance", param, commandType: CommandType.StoredProcedure);
                    statusCode = param.Get<int>("@Status");
                    idOut = param.Get<int>("@Returned_ID");
                message = param.Get<string>("@Message");
                }
                resp.statusCode.code = statusCode;
                resp.id = idOut;
                resp.statusCode.message = message;
                
            

            return resp;
        }
    }
}
