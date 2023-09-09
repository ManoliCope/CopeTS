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
using ProjectX.Entities.Models.Plan;

namespace ProjectX.Repository.PlanRepository
{
    public class PlanRepository : IPlanRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public PlanRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public PlanResp ModifyPlan(PlanReq req, string act, int userid)
        {
            var resp = new PlanResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@PL_Id", req.id);
            param.Add("@PL_Title", req.title.Trim());
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Plan_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.id = idOut;
            return resp;
        }
        public List<TR_Plan> GetPlanList(PlanSearchReq req)
        {
            var resp = new List<TR_Plan>();

            var param = new DynamicParameters();

            param.Add("@PL_Id", req.id);
            param.Add("@PL_Title", req.title);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Plan_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Plan>().ToList();

                }
            }

            return resp;
        }
        public TR_Plan GetPlan(int IdPlan)
        {
            var resp = new TR_Plan();
            var param = new DynamicParameters();
            param.Add("@PL_Id", IdPlan);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Plan_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_Plan>();
                }
            }

            return resp;
        }
    }
}
