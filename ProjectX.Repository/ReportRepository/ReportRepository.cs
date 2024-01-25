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
        public List<dynamic> GenerateProduction(productionReport req, int userid)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@prodid", req.prodId);
            param.Add("@userid", userid);
            param.Add("@datefrom", req.datefrom);
            param.Add("@dateto", req.dateto);
            param.Add("@agentid", req.agentId);
            param.Add("@subagentid", req.subAgentId);
            param.Add("@policynumber", req.policyNumber);
            param.Add("@policystatus", req.policyStatus);
            param.Add("@clientfirstname", req.clientFirstName);
            param.Add("@clientlastname", req.clientLastName);
            param.Add("@passportnumber", req.passportNumber);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Production", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
        public List<dynamic> GenerateBenefits(int userid)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@userid", userid);
            //param.Add("@datefrom", datefrom);
            //param.Add("@dateto", dateto);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Benefits", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
        public List<dynamic> GenerateBeneficiaries(int userid, productionReport req)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@userid", userid);
            param.Add("@agentid", req.agentId);
            param.Add("@subagentid", req.subAgentId);
            param.Add("@clientfirstname", req.clientFirstName);
            param.Add("@clientlastname", req.clientLastName);
            param.Add("@passportnumber", req.passportNumber);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Beneficiaries", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
        public List<dynamic> GenerateCurrencies(int userid,int req)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@userid", userid);
            param.Add("@req", req);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Currencies", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
     public List<dynamic> GenerateManualPolicies(int batchid)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@batchid", batchid);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_ManualPolicies", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
    }
}
