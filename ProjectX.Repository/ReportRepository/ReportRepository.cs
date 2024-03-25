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
using ProjectX.Entities.Models.General;

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
        public List<dynamic> GenerateTariff(int userid,int packageid, int planid, int assignedid, int productid)

        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@userid", userid);
            param.Add("@packageid", packageid);
            param.Add("@planid", planid);
            param.Add("@productid", productid);
            param.Add("@assignedid", assignedid);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_Tariff", param, commandType: CommandType.StoredProcedure))
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


        public LoadDataModel getChildren(int userid)
        {
            LoadDataModel resp = new LoadDataModel();
            //return resp;
            var param = new DynamicParameters();
            param.Add("@userid",userid);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_getChildren", param, commandTimeout: null, commandType: CommandType.StoredProcedure))
                {
                        resp.subagents = result.Read<LookUpp>().ToList();
                }
            }

            return resp;
        }
        public LoadDataModel getProducts(int userid)
        {
            LoadDataModel resp = new LoadDataModel();
            //return resp;
            var param = new DynamicParameters();
            param.Add("@userid",userid);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_getProducts", param, commandTimeout: null, commandType: CommandType.StoredProcedure))
                {
                        resp.products = result.Read<LookUpp>().ToList();
                }
            }

            return resp;
        }
        public List<dynamic> GeneratePrepaidAccounts(int U_Id, int userid, DateTime? datefrom, DateTime? dateto)
        {
            List<dynamic> response = new List<dynamic>();
            var param = new DynamicParameters();
            param.Add("@U_Id", U_Id);
            param.Add("@userid", userid);
            param.Add("@datefrom", datefrom);
            param.Add("@dateto", dateto);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Report_PrepaidAccounts", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<dynamic>().ToList();
                }
            }
            return response;
        }
    }
}
