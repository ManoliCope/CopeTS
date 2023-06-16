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
using ProjectX.Entities.Models.Benefit;

namespace ProjectX.Repository.BenefitRepository
{
    public class BenefitRepository : IBenefitRepository
    {
        private SqlConnection _db;
        private readonly CcAppSettings _appSettings;

        public BenefitRepository(IOptions<CcAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public BenResp ModifyBenefit(BenResp req)
        {
            var resp = new BenResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", "");
            param.Add("@B_Id", req.id);
            param.Add("@B_Title", req.title);
            param.Add("@B_Limit", req.limit);
            //param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            //param.Add("@idOut", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Benefit_CRUD", param, commandType: CommandType.StoredProcedure);
               // statusCode = param.Get<int>("@Status");
                //idOut = param.Get<int>("@idOut");
            }
           // resp.statusCode.code = statusCode;
            //resp.id = idOut;
            return resp;
        }
        public List<TR_Benefit> GetBenefit(BenReq req)
        {
            var resp = new List<TR_Benefit>();

            var param = new DynamicParameters();

            param.Add("@B_Id", req.id);
            param.Add("@B_Title", req.title);
            param.Add("@B_Limit", req.limit);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Benefit_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Benefit>().ToList();

                }
            }

            return resp;
        }

    }
}
