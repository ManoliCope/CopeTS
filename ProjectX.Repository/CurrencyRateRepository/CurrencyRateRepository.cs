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
using ProjectX.Entities.Models.CurrencyRate;

namespace ProjectX.Repository.CurrencyRateRepository
{
    public class CurrencyRateRepository : ICurrencyRateRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public CurrencyRateRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public CurrResp ModifyCurrencyRate(CurrReq req, string act, int userid)
        {
            var resp = new CurrResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@CR_Id", req.Id);
            param.Add("@CR_Currency_Id", req.Currency_Id);
            param.Add("@CR_Rate", req.Rate);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_CurrencyRate_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.Id = idOut;
            return resp;
        }
        public List<TR_CurrencyRate> GetCurrencyRateList(CurrSearchReq req)
        {
            var resp = new List<TR_CurrencyRate>();
            DateTime? nullableDateTime = null;
            var param = new DynamicParameters();

            param.Add("@CR_Id", req.Id);
            param.Add("@CR_Currency_Id", req.Currency_Id);
            param.Add("@CR_Rate", req.Rate);
            //param.Add("@CR_Creation_Date", null);
            //param.Add("@CR_Creation_Date", req.Creation_Date);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_CurrencyRate_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_CurrencyRate>().ToList();

                }
            }

            return resp;
        }
        public TR_CurrencyRate GetCurrencyRate(int IdCurrencyRate)
        {
            var resp = new TR_CurrencyRate();
            var param = new DynamicParameters();
            param.Add("@CR_Id", IdCurrencyRate);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_CurrencyRate_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_CurrencyRate>();
                }
            }

            return resp;
        }
    }
}
