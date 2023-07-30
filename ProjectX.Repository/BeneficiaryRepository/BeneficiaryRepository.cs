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
using ProjectX.Entities.Models.Beneficiary;

namespace ProjectX.Repository.BeneficiaryRepository
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public BeneficiaryRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public BeneficiaryResp ModifyBeneficiary(BeneficiaryReq req, string act, int userid)
        {
            var resp = new BeneficiaryResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@BE_Id", req.Id);
            param.Add("@BE_Sex", req.Sex);
            param.Add("@BE_FirstName", req.FirstName);
            param.Add("@BE_MiddleName", req.MiddleName);
            param.Add("@BE_LastName", req.LastName);
            param.Add("@BE_MaidenName", req.LastName);
            param.Add("@BE_PassportNumber", req.PassportNumber);
            param.Add("@BE_DOB", req.DateOfBirth);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Beneficiary_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.Id = idOut;
            return resp;
        }
        public List<TR_Beneficiary> GetBeneficiaryList(BeneficiarySearchReq req)
        {
            var resp = new List<TR_Beneficiary>();

            var param = new DynamicParameters();

            param.Add("@BE_Id", req.Id);
            param.Add("@BE_Sex", req.Id);
            param.Add("@BE_FirstName", req.FirstName);
            param.Add("@BE_MiddleName", req.MiddleName);
            param.Add("@BE_LastName", req.LastName);
            param.Add("@BE_MaidenName", req.LastName);
            param.Add("@BE_PassportNumber", req.PassportNumber);
            param.Add("@BE_DOB", req.DateOfBirth);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Beneficiary_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Beneficiary>().ToList();

                }
            }

            return resp;
        }
        public TR_Beneficiary GetBeneficiary(int IdBeneficiary)
        {
            var resp = new TR_Beneficiary();
            var param = new DynamicParameters();
            param.Add("@BE_Id", IdBeneficiary);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Beneficiary_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_Beneficiary>();
                }
            }

            return resp;
        }
        
        public BeneficiarySearchResp SearchBeneficiaryPref(string prefix)
        {
            var resp = new BeneficiarySearchResp();
            var param = new DynamicParameters();
            param.Add("@BE_Prefix", prefix);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Beneficiary_GetbyPerfix", param, commandType: CommandType.StoredProcedure))
                {
                    resp.beneficiary = result.Read<TR_Beneficiary>().ToList();
                }
            }

            return resp;
        }
    }
}
