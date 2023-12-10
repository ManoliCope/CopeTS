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
using Utilities;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.ProductionBatch;

namespace ProjectX.Repository.ProductionBatchRepository
{
    public class ProductionBatchRepository : IProductionBatchRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public ProductionBatchRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }


        //public ZoneResp ModifyZone(ZoneReq req, string act, int userid)
        //{
        //    DataTable dtDestinations = new DataTable();
        //    List<ListID> destinationsid = new List<ListID>();

        //    if (req.destinationId != null)
        //        foreach (int userId in req.destinationId)
        //        {
        //            destinationsid.Add(new ListID
        //            {
        //                ID = userId
        //            });
        //        }
        //    dtDestinations = ObjectConvertor.ListToDataTable<ListID>(destinationsid);


        //    var resp = new ZoneResp();
        //    int statusCode = 0;
        //    int idOut = 0;

        //    var param = new DynamicParameters();
        //    param.Add("@action", act);
        //    param.Add("@user_id", userid);
        //    param.Add("@Z_Id", req.id);
        //    param.Add("@Z_Title", req.title?.Trim());
        //    //param.Add("@Z_DestinationId", req.destinationId);
        //    param.Add("@Z_DestinationId", dtDestinations.AsTableValuedParameter("TR_IntegerID"));

        //    param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
        //    param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

        //    using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
        //    {
        //        _db.Execute("TR_ProductionBatch_CRUD", param, commandType: CommandType.StoredProcedure);
        //        statusCode = param.Get<int>("@Status");
        //        idOut = param.Get<int>("@Returned_ID");
        //    }
        //    resp.statusCode.code = statusCode;
        //    resp.id = idOut;
        //    return resp;
        //}
        public List<TR_ProductionBatch> GetProductionBatchList(ProductionBatchSearchReq req)
        {
            var resp = new List<TR_ProductionBatch>();

            var param = new DynamicParameters();

            param.Add("@PB_Id", req.id);
            param.Add("@PB_Title", req.name);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_ProductionBatch_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_ProductionBatch>().ToList();
                }
            }
            return resp;
        }
        public TR_ProductionBatch GetProductionBatch(int batchid)
        {
            var resp = new TR_ProductionBatch();
            var param = new DynamicParameters();
            param.Add("@PB_Id", batchid);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_ProductionBatch_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_ProductionBatch>();
                }
            }

            return resp;
        }
    }
}
