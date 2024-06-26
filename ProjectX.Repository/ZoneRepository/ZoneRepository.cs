﻿using ProjectX.Entities.AppSettings;
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
using Utilities;
using ProjectX.Entities.Models.General;

namespace ProjectX.Repository.ZoneRepository
{
    public class ZoneRepository : IZoneRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public ZoneRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }


        public ZoneResp ModifyZone(ZoneReq req, string act, int userid)
        {
            DataTable dtDestinations = new DataTable();
            List<ListID> destinationsid = new List<ListID>();

            if (req.destinationId != null)
                foreach (int userId in req.destinationId)
                {
                    destinationsid.Add(new ListID
                    {
                        ID = userId
                    });
                }
            dtDestinations = ObjectConvertor.ListToDataTable<ListID>(destinationsid);


            var resp = new ZoneResp();
            int statusCode = 0;
            int idOut = 0;

            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@Z_Id", req.id);
            param.Add("@Z_Title", req.title?.Trim());
            //param.Add("@Z_DestinationId", req.destinationId);
            param.Add("@Z_DestinationId", dtDestinations.AsTableValuedParameter("TR_IntegerID"));

            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Zone_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.id = idOut;
            return resp;
        }
        public List<TR_Zone> GetZoneList(ZoneSearchReq req)
        {
            var resp = new List<TR_Zone>();

            var param = new DynamicParameters();

            param.Add("@Z_id", req.id);
            param.Add("@Z_Title", req.title);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Zone_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Zone>().ToList();
                    var dest = result.Read<TR_ZoneDestination>().ToList();
                    if (resp.Any() && dest.Any())
                    {
                        foreach (var res in resp)
                        {
                            res.Z_Destination_Id = dest.Where(a => a.Z_Id == res.Z_Id).Select(a => a.D_Id).ToList();
                        }
                    }

                }

            }
            return resp;
        }
        public TR_Zone GetZone(int IdZone)
        {
            var resp = new TR_Zone();
            var param = new DynamicParameters();
            param.Add("@Z_id", IdZone);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Zone_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_Zone>();
                    resp.Z_Destination_Id = result.Read<int>().ToList();
                }
            }

            return resp;
        }
    }
}
