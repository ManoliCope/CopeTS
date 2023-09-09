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
using ProjectX.Entities.Models.BenefitTitle;

namespace ProjectX.Repository.BenefitTitleRepository
{
    public class BenTitleRepository : IBenTitleRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public BenTitleRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public BenTitleResp ModifyBenTitle(BenTitleReq req, string act, int userid)
        {
            var resp = new BenTitleResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@BT_Id", req.id);
            param.Add("@BT_Title", req.title.Trim());
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_BenTitle_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.id = idOut;
            return resp;
        }
        public List<TR_BenefitTitle> GetBenTitleList(BenTitleSearchReq req)
        {
            var resp = new List<TR_BenefitTitle>();

            var param = new DynamicParameters();

            param.Add("@BT_Id", req.id);
            param.Add("@BT_Title", req.title);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_BenTitle_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_BenefitTitle>().ToList();

                }
            }

            return resp;
        }
        public TR_BenefitTitle GetBenTitle(int IdTitle)
        {
            var resp = new TR_BenefitTitle();
            var param = new DynamicParameters();
            param.Add("@BT_Id", IdTitle);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_BenTitle_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_BenefitTitle>();
                }
            }

            return resp;
        }
    }
}
