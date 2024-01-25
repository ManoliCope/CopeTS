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
using ProjectX.Entities.Models.Package;

namespace ProjectX.Repository.PackageRepository
{
    public class PackageRepository : IPackageRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public PackageRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public PackResp ModifyPackage(PackReq req, string act, int userid)
        {
            var resp = new PackResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@P_Id", req.Id);
            param.Add("@P_Name", req.Name?.Trim());
            param.Add("@PR_Id", req.ProductId);
            param.Add("@P_ZoneID", req.ZoneID);
            param.Add("@P_Remove_deductable", req.Remove_deductable);
            param.Add("@P_Remove_Sports_Activities", req.Remove_Sports_Activities);
            param.Add("@P_Adult_No", req.Adult_No);
            param.Add("@P_Children_No", req.Children_No);
            param.Add("@P_PA_Included", req.PA_Included);
            param.Add("@P_Adult_Max_Age", req.Adult_Max_Age);
            param.Add("@P_Child_Max_Age", req.Child_Max_Age);
            param.Add("@P_Special_Case", req.Special_Case);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Package_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.Id = idOut;
            return resp;
        }
        public List<TR_Package> GetPackageList(PackSearchReq req)
        {
            var resp = new List<TR_Package>();

            var param = new DynamicParameters();
            param.Add("@P_Id", req.Id);
            param.Add("@P_Name", req.Name);
            param.Add("@PR_Id", req.ProductId);
            param.Add("@P_ZoneID", req.ZoneID);
            param.Add("@P_Remove_deductable", req.Remove_deductable);
            param.Add("@P_Remove_Sports_Activities", req.Remove_Sports_Activities);
            param.Add("@P_Adult_No", req.Adult_No);
            param.Add("@P_Children_No", req.Children_No);
            param.Add("@P_PA_Included", req.PA_Included);
            param.Add("@P_Adult_Max_Age", req.Adult_Max_Age);
            param.Add("@P_Child_Max_Age", req.Child_Max_Age);
            param.Add("@P_Special_Case", req.Special_Case);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Package_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Package>().ToList();

                }


            }

            return resp;
        }
        public TR_Package GetPackage(int IdPackage)
        {
            var resp = new TR_Package();
            var param = new DynamicParameters();
            param.Add("@P_id", IdPackage);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Package_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_Package>();
                }
            }

            return resp;
        }
    }
}
