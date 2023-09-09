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
using ProjectX.Entities.Resources;
using ProjectX.Entities;

namespace ProjectX.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public ProductRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public ProdResp ModifyProduct(ProdReq req,string act,int userid)
        {
            var resp = new ProdResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@id", req.id);
            param.Add("@title", req.title.Trim());
            param.Add("@description", req.description);
            param.Add("@is_family", req.is_family);
            param.Add("@pr_activation_date", req.activation_date);
            param.Add("@pr_is_active", req.is_active);
            param.Add("@pr_sports_activities", req.sports_activities);
            param.Add("@pr_additional_benefits", req.additional_benefits);
            param.Add("@pr_is_deductible", req.is_deductible);

            param.Add("@pr_is_individual", req.Is_Individual);
            param.Add("@pr_is_group", req.Is_Group);
            param.Add("@pr_deductible_format", req.Deductible_Format);
            param.Add("@pr_sports_activity_format", req.Sports_Activity_Format);
            param.Add("@pr_additional_benefits_format", req.Additional_Benefits_Format);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Product_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.id = idOut;

            return resp;
        }
        public List<TR_Product> GetProductList(ProdSearchReq req)
        {
            var resp = new List<TR_Product>();
            var param = new DynamicParameters();
            param.Add("@pr_id", req.id);
            param.Add("@pr_title", req.title);
            param.Add("@pr_description", req.description);
            param.Add("@pr_is_family", req.is_family);
            param.Add("@pr_is_active", req.is_active);
            param.Add("@pr_sports_activities", req.sports_activities);
            param.Add("@pr_additional_benefits", req.additional_benefits);

            param.Add("@pr_is_individual", req.Is_Individual);
            param.Add("@pr_is_group", req.Is_Group);
            param.Add("@pr_deductible_format", req.Deductible_Format);
            param.Add("@pr_sports_activity_format", req.Sports_Activity_Format);
            param.Add("@pr_additional_benefits_format", req.Additional_Benefits_Format);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Product_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Product>().ToList();

                }
            }

            return resp;
        }

        public TR_Product GetProduct(int Idproduct)
        {
            var resp = new TR_Product();
            var param = new DynamicParameters();
            param.Add("@pr_id", Idproduct);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Product_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_Product>();
                }
            }

            return resp;
        }
    }
}
