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
using ProjectX.Entities.Models.Production;
using ProjectX.Entities.Resources;
using ProjectX.Entities;

namespace ProjectX.Repository.ProductionRepository
{
    public class ProductionRepository : IProductionRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public ProductionRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public List<TR_Product> GetProductsByType(int idType)
        {
            List<TR_Product> response = new List<TR_Product>();
            //var resp = new TR_Product();
            var param = new DynamicParameters();
            param.Add("@type", idType);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Products_GetbyType", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<TR_Product>().ToList();
                }
            }

            return response;
        }   
        public List<TR_Destinations> GetDestinationByZone(int idZone)
        {
            List<TR_Destinations> response = new List<TR_Destinations>();
            var param = new DynamicParameters();
            param.Add("@zone", idZone);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Destinations_GetbyZone", param, commandType: CommandType.StoredProcedure))
                {
                    response = result.Read<TR_Destinations>().ToList();
                }
            }

            return response;
        }
   
        public ProductionResp getProductionDetails (ProductionReq req)
        {
            ProductionResp response = new ProductionResp();
            var param = new DynamicParameters();
            param.Add("@zone", req.Zone);
            param.Add("@product", req.Product);
            param.Add("@age", req.Ages);
            param.Add("@durations", req.Durations);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_GetProductionDetails", param, commandType: CommandType.StoredProcedure))
                {
                    response.tariff = result.Read<TR_Tariff>().ToList();
                    response.production = req;
                }
            }

            return response;
        }
    }
}
