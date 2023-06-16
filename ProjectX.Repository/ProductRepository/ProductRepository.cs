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
using ProjectX.Entities.Models.Product;

namespace ProjectX.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private SqlConnection _db;
        private readonly CcAppSettings _appSettings;

        public ProductRepository(IOptions<CcAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public ProdResp ModifyProduct(ProdResp req)
        {
            var resp = new ProdResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", "");
            param.Add("@id", req.id);
            param.Add("@title", req.title);
            param.Add("@description", req.description);
            param.Add("@is_family", req.is_family);
            param.Add("@pr_activation_date", req.activation_date);
            param.Add("@pr_is_active", req.is_active);
            param.Add("@pr_sports_activities", req.sports_activities);
            param.Add("@pr_additional_benefits", req.additional_benefits);
            //param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            //param.Add("@idOut", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Product_CRUD", param, commandType: CommandType.StoredProcedure);
              //  statusCode = param.Get<int>("@Status");
               // idOut = param.Get<int>("@idOut");
            }
            //resp.statusCode.code = statusCode;
            //resp.id = idOut;
            return resp;
        }
        public List<TR_Product> GetProduct(ProdReq req)
        {
            var resp = new List<TR_Product>();

            var param = new DynamicParameters();

            param.Add("@id", req.id);
            param.Add("@title", req.title);
            param.Add("@description", req.description);
            param.Add("@is_family", req.is_family);
            param.Add("@pr_activation_date", req.activation_date);
            param.Add("@pr_is_active", req.is_active);
            param.Add("@pr_sports_activities", req.sports_activities);
            param.Add("@pr_additional_benefits", req.additional_benefits);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Product_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Product>().ToList();

                }
            }

            return resp;
        }

    }
}
