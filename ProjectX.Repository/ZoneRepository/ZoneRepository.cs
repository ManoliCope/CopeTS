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
using ProjectX.Entities.Models.Zone;

namespace ProjectX.Repository.ZoneRepository
{
    public class ZoneRepository : IZoneRepository
    {
        private SqlConnection _db;
        private readonly CcAppSettings _appSettings;

        public ZoneRepository(IOptions<CcAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public ZoneResp ModifyZone(ZoneResp req)
        {
            var resp = new ZoneResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", "");
            param.Add("@Z_id", req.id);
            param.Add("@Z_Title", req.title);
            param.Add("@Z_Destination_Id", req.destinationId);

            //param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            //param.Add("@idOut", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Product_CRUD", param, commandType: CommandType.StoredProcedure);
                //statusCode = param.Get<int>("@Status");
                //idOut = param.Get<int>("@idOut");
            }
            //resp.statusCode.code = statusCode;
            //resp.id = idOut;
            return resp;
        }
        public List<TR_Zone> GetZone(ZoneReq req)
        {
            var resp = new List<TR_Zone>();

            var param = new DynamicParameters();

            param.Add("@Z_id", req.id);
            param.Add("@Z_Title", req.title);
            param.Add("@Z_Destination_Id", req.destinationId);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Zone_Get", param, commandType: CommandType.StoredProcedure))
                {
                     resp = result.Read<TR_Zone>().ToList(); 

                }
               
            }
            return resp;
        }
    }
}
