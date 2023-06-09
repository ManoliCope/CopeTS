using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.bModels
{
    public class LoadDataModelSetup
    {
        public bool loadCaseStatuses { get; set; }
        public bool loadTravelCaseTypes { get; set; }
        public bool loadCaseComplexities { get; set; }
        public bool loadApprovalStatuses { get; set; }
        public bool loadCountries { get; set; }
        public bool loadDialCodes { get; set; }
        public bool loadFeesTypes { get; set; }
        public bool LoadGenders { get; set; }
        public bool loadProfileTypes { get; set; }
        public bool loadUserTypes { get; set; }
        public bool loadCustomerTypes { get; set; }
        public bool loadBenefits { get; set; }
        public bool loadCurrencies { get; set; }
        public bool loadDocumentTypes { get; set; }
        public bool loadObjectReferences { get; set; }
        public bool loadProfiles { get; set; }
        public bool loadPayers { get; set; }
        public bool loadPartners { get; set; }
        public bool loadTextReplacements { get; set; }
        public bool loadDepartments { get; set; }
        public bool loadProducts { get; set; }
        public bool loadAdditionalCoverage { get; set; }
        

        #region Motor 
        public bool loadMotorCaseTypes { get; set; }
        public bool loadInsuranceLines { get; set; }
        public bool loadCallCategories { get; set; }
        public bool loadMotorSendEmailTo { get; set; }
        public bool loadRegions { get; set; }
        public bool loadLocations { get; set; }
        public bool loadCities { get; set; }
        public bool loadTowingCompanies { get; set; }
        public bool loadWorkShops { get; set; }
        public bool loadExperts { get; set; }
        public bool loadExpertsRejectionReasons { get; set; }
        public bool loadCarPlateCodes { get; set; }
        public bool loadCobs { get; set; }
        public bool loadPolicyServices { get; set; }
        public bool loadTowingStatuses { get; set; }
        public bool loadExpertStatuses { get; set; }
        public bool loadTowingTypes { get; set; }
        public bool loadFeedbackConfigs { get; set; }
        public bool loadNoFeedbackReasons { get; set; }
        public bool loadPositiveFeedbacks { get; set; }
        public bool loadNegativeFeedbacks { get; set; }
        public bool loadDiagnosis { get; set; }
        public bool loadGeneralStatus { get; set; }
        public bool loadLineofBusiness { get; set; }
        #endregion
        #region TRAVEL
        public bool loadFinancialEmail { get; set; }
        #endregion
    }
}
