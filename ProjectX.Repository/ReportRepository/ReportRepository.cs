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
using ProjectX.Entities.Models.Report;

namespace ProjectX.Repository.ReportRepository
{
    public class ReportRepository : IReportRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public ReportRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public List<dynamic> GenerateProduction(int req, int userid)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@prodid", req);
            param.Add("@userid", userid);
           

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Production", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
        public List<dynamic> GenerateBenefits( int userid)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@userid", userid);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Benefits", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
        public List<dynamic> GenerateBeneficiaries(int userid)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@userid", userid);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Beneficiaries", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
    }
}
