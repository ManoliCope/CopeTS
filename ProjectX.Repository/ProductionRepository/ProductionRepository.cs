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
using ProjectX.Repository.BeneficiaryRepository;
using ProjectX.Repository.GeneralRepository;
using System.Collections;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography;

namespace ProjectX.Repository.ProductionRepository
{
    public class ProductionRepository : IProductionRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;
        private IGeneralRepository _generalRepository;

        public ProductionRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralRepository generalrepository)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
            _generalRepository = generalrepository;
        }

        public List<TR_PolicyHeader> GetPoliciesList(ProductionSearchReq req, int userid)
        {
            var resp = new List<TR_PolicyHeader>();

            var param = new DynamicParameters();
            param.Add("@Pol_Reference", req.Reference);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Production_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_PolicyHeader>().ToList();
                }
            }
            return resp;
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
        public List<TR_Zone> GetZonesByProduct(int idType)
        {
            List<TR_Zone> response = new List<TR_Zone>();
            //var resp = new TR_Product();
            var param = new DynamicParameters();
            param.Add("@productid", idType);

            try
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Zones_GetbyProduct", param, commandType: CommandType.StoredProcedure))
                    {
                        response = result.Read<TR_Zone>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log the error, show an error message, etc.)
                Console.WriteLine("Error occurred while connecting to the database: " + ex.Message);
                return null;
            }


            return response;
        }
        public List<TR_Destinations> GetDestinationByZone(int idZone)
        {
            List<TR_Destinations> response = new List<TR_Destinations>();
            var param = new DynamicParameters();
            param.Add("@zone", idZone);

            try
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Destinations_GetbyZone", param, commandType: CommandType.StoredProcedure))
                    {
                        response = result.Read<TR_Destinations>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log the error, show an error message, etc.)
                Console.WriteLine("Error occurred while connecting to the database: " + ex.Message);
                return null;
            }


            return response;
        }

        public ProductionResp getProductionDetails(List<ProductionReq> req, int userid)
        {
            ProductionResp response = new ProductionResp();
            using (SqlConnection connection = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                //var param = new DynamicParameters();
                //param.Add("@Zone", req.Zone);
                //param.Add("@Product", req.Product);
                //param.Add("@Ages", _generalRepository.ToDataTable(req.Ages));
                //param.Add("@Durations", _generalRepository.ToDataTable(req.Durations));

                DataTable dataTable = ConvertToDataTable(req);

                var queryParameters = new DynamicParameters(new
                {
                    //Zones = req.InsuredQuotations.Select(iq => iq.Zone).ToList(),
                    //Products = req.InsuredQuotations.Select(iq => iq.Product).ToList(),
                    //Ages = req.InsuredQuotations.Select(iq => iq.Ages).ToList(),
                    //Durations = req.InsuredQuotations.Select(iq => iq.Durations).ToList(),
                    QuoteReq = dataTable,
                    IdUser = userid

                });

                var query = "TR_GetProductionDetails";

                using (SqlMapper.GridReader result = connection.QueryMultiple(query, queryParameters, commandType: CommandType.StoredProcedure))
                {
                    response.QuotationResp = result.Read<QuotationResp>().ToList();
                    response.AdditionalBenefits = result.Read<TR_Benefit>().ToList();
                }
            }

            return response;
        }

        public static DataTable ConvertIntListToDataTable(List<int> intList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Value", typeof(int));

            foreach (int value in intList)
            {
                dataTable.Rows.Add(value);
            }

            return dataTable;
        }

        public ProductionResp SaveIssuance(IssuanceReq IssuanceReq, int userid)
        {
            ProductionResp response = new ProductionResp();
            string thisresult = "";


            DataTable test = ConvertIntListToDataTable(IssuanceReq.selectedDestinationIds);
            DataTable additionalDT = new DataTable();
            if (IssuanceReq.additionalBenefits != null)
                additionalDT = ConvertToDataTable(IssuanceReq.additionalBenefits);
            else
            {
                additionalDT.Columns.Add("insuredid", typeof(int));
                additionalDT.Columns.Add("value", typeof(string));
                additionalDT.Columns.Add("price", typeof(double));
            }

            using (SqlConnection connection = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                //var param = new DynamicParameters();
                //param.Add("@Zone", req.Zone);
                //param.Add("@Product", req.Product);
                //param.Add("@Ages", _generalRepository.ToDataTable(req.Ages));
                //param.Add("@Durations", _generalRepository.ToDataTable(req.Durations));
                var queryParameters = new DynamicParameters(new
                {
                    InsuredData = ConvertToDataTable(IssuanceReq.beneficiaryDetails),
                    BeneficiaryData = ConvertToDataTable(IssuanceReq.beneficiaryData),
                    AdditionalBenefit = additionalDT,

                    SelectedDestinationIds = ConvertIntListToDataTable(IssuanceReq.selectedDestinationIds),
                    SelectedDestinations = IssuanceReq.selectedDestinations,
                    Duration = IssuanceReq.duration,
                    ToDate = IssuanceReq.to,
                    FromDate = IssuanceReq.from,
                    IsFamily = IssuanceReq.is_family,
                    IsIndividual = IssuanceReq.Is_Individual,
                    IsGroup = IssuanceReq.Is_Group,
                    ProductId = IssuanceReq.productId,
                    ZoneId = IssuanceReq.zoneId,
                    InitialPremium = IssuanceReq.InitialPremium,
                    AdditionalValue = IssuanceReq.AdditionalValue,
                    TaxVATValue = IssuanceReq.TaxVATValue,
                    StampsValue = IssuanceReq.StampsValue,
                    GrandTotal = IssuanceReq.GrandTotal,
                    Userid = userid
                });

                var query = "TR_SaveIssuanceProcedure";

                using (SqlMapper.GridReader result = connection.QueryMultiple(query, queryParameters, commandType: CommandType.StoredProcedure))
                {
                    thisresult = result.Read<string>().First();
                    //response.AdditionalBenefits = result.Read<TR_Benefit>().ToList();
                }
            }

            return response;
        }


        public ProductionPolicy GetPolicy(int IdPolicy, int userid)
        {
            var resp = new ProductionPolicy();
            var param = new DynamicParameters();
            param.Add("@PolicyID", IdPolicy);
            param.Add("@Userid", userid);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Production_GetPolicy", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<ProductionPolicy>().SingleOrDefault();
                    if (resp != null)
                    {
                        resp.PolicyDetails = result.Read<PolicyDetail>().ToList();
                        resp.AdditionalBenefits = result.Read<PolicyAdditionalBenefit>().ToList();
                        resp.Destinations = result.Read<PolicyDestination>().ToList();
                    }
                }
            }

            return resp;
        }



        public static DataTable ConvertToDataTable<T>(IEnumerable<T> list)
        {
            DataTable dataTable = new DataTable();

            // Get all the public properties of the class using reflection
            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create data columns for the DataTable using the property names and types
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                dataTable.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }

            // Add data rows to the DataTable
            foreach (T item in list)
            {
                DataRow dataRow = dataTable.NewRow();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(item);
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }




    }
}
