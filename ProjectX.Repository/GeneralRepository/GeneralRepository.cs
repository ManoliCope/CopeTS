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

namespace ProjectX.Repository.GeneralRepository
{
    public class GeneralRepository : IGeneralRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public GeneralRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public LoadDataModel loadData(LoadDataModelSetup loadDataModelSetup)
        {

            LoadDataModel resp = new LoadDataModel();
            var param = new DynamicParameters();
            param.Add("@loadCaseStatuses", loadDataModelSetup.loadCaseStatuses);
            param.Add("@loadTravelCaseTypes", loadDataModelSetup.loadTravelCaseTypes);
            param.Add("@loadCaseComplexities", loadDataModelSetup.loadCaseComplexities);
            param.Add("@loadApprovalStatuses", loadDataModelSetup.loadApprovalStatuses);
            param.Add("@loadCountries", loadDataModelSetup.loadCountries);
            param.Add("@loadDialCodes", loadDataModelSetup.loadDialCodes);
            param.Add("@loadFeesTypes", loadDataModelSetup.loadFeesTypes);
            param.Add("@loadProfileTypes", loadDataModelSetup.loadProfileTypes);
            param.Add("@loadUserTypes", loadDataModelSetup.loadUserTypes);
            param.Add("@LoadGenders", loadDataModelSetup.LoadGenders);
            param.Add("@loadCustomerType", loadDataModelSetup.loadCustomerTypes);
            param.Add("@loadBenefits", loadDataModelSetup.loadBenefits);
            param.Add("@loadCurrencies", loadDataModelSetup.loadCurrencies);
            param.Add("@loadDocumentTypes", loadDataModelSetup.loadDocumentTypes);
            param.Add("@loadObjectReferences", loadDataModelSetup.loadObjectReferences);
            param.Add("@loadProfiles", loadDataModelSetup.loadProfiles);
            param.Add("@loadPayers", loadDataModelSetup.loadPayers);
            param.Add("@loadPartners", loadDataModelSetup.loadPartners);
            param.Add("@loadTextReplacements", loadDataModelSetup.loadTextReplacements);
            param.Add("@loadDepartments", loadDataModelSetup.loadDepartments);
            param.Add("@loadMotorCaseTypes", loadDataModelSetup.loadMotorCaseTypes);
            param.Add("@loadInsuranceLines", loadDataModelSetup.loadInsuranceLines);
            param.Add("@loadCallCategories", loadDataModelSetup.loadCallCategories);
            param.Add("@loadMotorSendEmailTo", loadDataModelSetup.loadMotorSendEmailTo);
            param.Add("@loadRegions", loadDataModelSetup.loadRegions);
            param.Add("@loadLocations", loadDataModelSetup.loadLocations);
            param.Add("@loadCities", loadDataModelSetup.loadCities);
            param.Add("@loadTowingCompanies", loadDataModelSetup.loadTowingCompanies);
            param.Add("@loadWorkShops", loadDataModelSetup.loadWorkShops);
            param.Add("@loadExperts", loadDataModelSetup.loadExperts);
            param.Add("@loadExpertsRejectionReasons", loadDataModelSetup.loadExpertsRejectionReasons);
            param.Add("@loadCarPlateCodes", loadDataModelSetup.loadCarPlateCodes);
            param.Add("@loadCobs", loadDataModelSetup.loadCobs);
            param.Add("@loadPolicyServices", loadDataModelSetup.loadPolicyServices);
            param.Add("@loadTowingStatuses", loadDataModelSetup.loadTowingStatuses);
            param.Add("@loadExpertStatuses", loadDataModelSetup.loadTowingStatuses);
            param.Add("@loadTowingTypes", loadDataModelSetup.loadTowingTypes);
            param.Add("@loadFeedbackConfigs", loadDataModelSetup.loadFeedbackConfigs);
            param.Add("@loadNoFeedbackReasons", loadDataModelSetup.loadNoFeedbackReasons);
            param.Add("@loadProducts", loadDataModelSetup.loadProducts);
            param.Add("@loadPositiveFeedbacks", loadDataModelSetup.loadPositiveFeedbacks);
            param.Add("@loadNegativeFeedbacks", loadDataModelSetup.loadNegativeFeedbacks);
            param.Add("@loadDiagnosis", loadDataModelSetup.loadDiagnosis);
            param.Add("@loadgeneralStatus", loadDataModelSetup.loadGeneralStatus);
            param.Add("@loadLineofBusiness", loadDataModelSetup.loadLineofBusiness);
            param.Add("@loadAdditionalCoverage", loadDataModelSetup.loadAdditionalCoverage);
            param.Add("@loadFinancialEmail", loadDataModelSetup.loadFinancialEmail);
            

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_data_load", param, commandTimeout: null, commandType: CommandType.StoredProcedure))
                {
                    if (loadDataModelSetup.loadCaseStatuses)
                        resp.caseStatuses = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadTravelCaseTypes)
                        resp.caseTypes = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadCaseComplexities)
                        resp.caseComplexities = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadApprovalStatuses)
                        resp.ApprovalStatuses = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadCountries)
                    //    resp.countries = result.Read<Country>().ToList();
                    //if (loadDataModelSetup.loadDialCodes)
                    //    resp.dialCodes = result.Read<Country>().ToList();
                    if (loadDataModelSetup.loadFeesTypes)
                        resp.feesTypes = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadProfileTypes)
                        resp.profileTypes = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadUserTypes)
                        resp.userTypes = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.LoadGenders)
                        resp.genders = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadCustomerTypes)
                        resp.customerTypes = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadBenefits)
                    //    resp.benefits = result.Read<Benefit>().ToList();
                    //if (loadDataModelSetup.loadCurrencies)
                    //    resp.currencies = result.Read<Currency>().ToList();
                    if (loadDataModelSetup.loadDocumentTypes)
                        resp.documentTypes = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadObjectReferences)
                        resp.objectReferences = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadProfiles)
                        resp.profiles = result.Read<Profile>().ToList();
                    if (loadDataModelSetup.loadPayers)
                        resp.payers = result.Read<Profile>().ToList();
                    if (loadDataModelSetup.loadPartners)
                        resp.partners = result.Read<Profile>().ToList();
                    if (loadDataModelSetup.loadTextReplacements)
                        resp.textReplacements = result.Read<TextReplacement>().ToList();
                    if (loadDataModelSetup.loadDepartments)
                        resp.departments = result.Read<LookUpp>().ToList();

                    if (loadDataModelSetup.loadMotorCaseTypes)
                        resp.motorCaseTypes = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadInsuranceLines)
                        resp.InsuranceLines = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadCallCategories)
                        resp.CallCategories = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadMotorSendEmailTo)
                    //    resp.MotorSendEmailTo = result.Read<MotorSendEmtailTo>().ToList();
                    //if (loadDataModelSetup.loadRegions)
                    //    resp.Regions = result.Read<Location>().ToList();
                    //if (loadDataModelSetup.loadLocations)
                    //    resp.Locations = result.Read<Location>().ToList();
                    //if (loadDataModelSetup.loadCities)
                    //    resp.Cities = result.Read<Location>().ToList();
                    if (loadDataModelSetup.loadTowingCompanies)
                        resp.TowingCompanies = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadWorkShops)
                        resp.WorkShops = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadExperts)
                    //    resp.Experts = result.Read<ExpertByRegion>().ToList();
                    if (loadDataModelSetup.loadExpertsRejectionReasons)
                        resp.ExpertsRejectionReasons = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadCarPlateCodes)
                    //    resp.carPlateCodes = result.Read<SysTab>().ToList();
                    //if (loadDataModelSetup.loadCobs)
                    //    resp.cobs = result.Read<SysTab>().ToList();
                    if (loadDataModelSetup.loadPolicyServices)
                        resp.policyServices = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadTowingStatuses)
                        resp.towingStatuses = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadExpertStatuses)
                        resp.expertStatuses = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadTowingTypes)
                        resp.towingTypes = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadFeedbackConfigs)
                    //    resp.feedbackConfigs = result.Read<FeedbackConfig>().ToList();
                    if (loadDataModelSetup.loadNoFeedbackReasons)
                        resp.noFeedbackReasons = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadProducts)
                        resp.Products = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadPositiveFeedbacks)
                        resp.positiveFeedbacks = result.Read<LookUpp>().ToList();
                    if (loadDataModelSetup.loadNegativeFeedbacks)
                        resp.negativeFeedbacks = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadDiagnosis)
                    //    resp.Diagnosis = result.Read<Diagnosis>().ToList();
                    if (loadDataModelSetup.loadGeneralStatus)
                        resp.GeneralStatus = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadLineofBusiness)
                    //    resp.LineofBusiness = result.Read<SysTab>().ToList();
                    if (loadDataModelSetup.loadAdditionalCoverage)
                        resp.AdditionalCoverages = result.Read<LookUpp>().ToList();
                    //if (loadDataModelSetup.loadFinancialEmail)
                    //    resp.financialEmail = result.Read<FinancialEmail>().ToList();
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
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_app_config_get", null, commandType: CommandType.StoredProcedure))
                {
                    return result.Read<AppConfig>().ToList();
                }
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
    }
}
