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
using Microsoft.AspNetCore.Http;
using ProjectX.Entities.Models;
//using NLog;
//using Microsoft.Extensions.Logging;

namespace ProjectX.Repository.GeneralRepository
{
    public class GeneralRepository : IGeneralRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public GeneralRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public LoadDataModel loadData(LoadDataModelSetup loadDataModelSetup)
        {

            LoadDataModel resp = new LoadDataModel();
            //return resp;
            var param = new DynamicParameters();
            param.Add("@loadBenefits", loadDataModelSetup.loadBenefits);
            param.Add("@loadProducts", loadDataModelSetup.loadProducts);
            param.Add("@loadPlans", loadDataModelSetup.loadPlans);
            param.Add("@loadPackages", loadDataModelSetup.loadPackages);
            param.Add("@loadTariffs", loadDataModelSetup.loadTariffs);
            param.Add("@loadUsers", loadDataModelSetup.loadUsers);
            param.Add("@loadZones", loadDataModelSetup.loadZones);
            param.Add("@loadDestinations", loadDataModelSetup.loadDestinations);
            param.Add("@loadSexNames", loadDataModelSetup.loadSexNames);
            param.Add("@loadFormats", loadDataModelSetup.loadFormats);
            param.Add("@loadUserCategory", loadDataModelSetup.loadUserCategory);
            param.Add("@loadRoundingRule", loadDataModelSetup.loadRoundingRule);
            param.Add("@loadSuperAgents", loadDataModelSetup.loadSuperAgents);
            param.Add("@loadCurrencies", loadDataModelSetup.loadCurrencies);
            param.Add("@loadCurrencyRate", loadDataModelSetup.loadCurrencyRate);
            param.Add("@loadBenefitTitle", loadDataModelSetup.loadBenefitTitle);
            param.Add("@loadProductionTabs", loadDataModelSetup.loadProductionTabs);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_data_load", param, commandTimeout: null, commandType: CommandType.StoredProcedure))
                {
                    if (loadDataModelSetup.loadBenefits)
                        resp.benefits = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadProducts)
                        resp.products = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadPlans)
                        resp.plans = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadPackages)
                        resp.packages = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadUsers)
                        resp.users = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadZones)
                        resp.zones = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadDestinations)
                        resp.destinations = result.Read<Destination>().ToList();
                    if (loadDataModelSetup.loadSexNames)
                        resp.sex = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadFormats)
                        resp.format = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadUserCategory)
                        resp.userCategory = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadRoundingRule)
                        resp.roundingRule = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadSuperAgents)
                        resp.superAgents = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadCurrencies)
                        resp.currencies = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadCurrencyRate)
                        resp.currencyRate = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadBenefitTitle)
                        resp.benefitTitle = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadProductionTabs)
                        resp.productionTabs = result.Read<LookUpp>().ToList();
                }
            }

            //foreach (Country country in resp.countries)
            //{
            //    List<CountryData> t = FamFamFam.Flags.Wpf.CountryData.AllCountries.ToList();
            //    CountryData tr = t.Where(x => x.Iso2 == country.Code).FirstOrDefault();
            //    var r = new RegionInfo(country.Code);
            //    var flagName = r.TwoLetterISORegionName + ".gif";
            //    //FamFamFam.Flags.Wpf.CountryIdToFlagImageSourceConverter countryIdToFlagImageSourceConverter = new CountryIdToFlagImageSourceConverter();
            //    //var yy = countryIdToFlagImageSourceConverter.Convert();
            //}

            return resp;
        }

        public IList<AppConfig> GetAppConfigs()
        {
            try
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    using (SqlMapper.GridReader result = _db.QueryMultiple("tr_app_config_get", null, commandType: CommandType.StoredProcedure))
                    {
                        return result.Read<AppConfig>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                loggertest logger = new loggertest();
                logger.Log("Error: " + ex.Message);
                logger.Log("Error: " + ex.StackTrace.ToString());

                return null;
            }

           
        }

        public IList<FileDirectory> GetFileDirectories()
        {
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_file_directory_get", null, commandType: CommandType.StoredProcedure))
                {
                    return result.Read<FileDirectory>().ToList();
                }
            }
        }

        public IList<EmailTemplate> GetEmailTemplates()
        {
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_email_template_get", null, commandType: CommandType.StoredProcedure))
                {
                    return result.Read<EmailTemplate>().ToList();
                }
            }
        }

        public IList<TextReplacement> GetTextReplacements()
        {
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_text_replacement_get", null, commandType: CommandType.StoredProcedure))
                {
                    return result.Read<TextReplacement>().ToList();
                }
            }
        }

        public DataTable ToDataTable(List<int> list)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Value", typeof(int));
            foreach (int item in list)
            {
                dataTable.Rows.Add(item);
            }
            return dataTable;
        }

        public void LogErrorToDatabase(LogData logData)
        {
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Open();

                var param = new DynamicParameters();
                param.Add("@Timestamp", logData.Timestamp);
                param.Add("@Controller", logData.Controller);
                param.Add("@Action", logData.Action);
                param.Add("@ErrorMessage", logData.ErrorMessage);
                param.Add("@Type", logData.Type);
                param.Add("@Message", logData.Message);
                param.Add("@RequestPath", logData.RequestPath);
                param.Add("@Response", logData.Response);
                param.Add("@Exception", logData.Exception);
                param.Add("@ExecutionTime", logData.ExecutionTime);
                param.Add("@Userid", logData.Userid);

                _db.Execute(
                    "TR_LogError",param,commandType: CommandType.StoredProcedure
                );
            }

        }

    }
}
