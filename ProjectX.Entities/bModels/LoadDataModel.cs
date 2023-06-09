using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.bModels
{
    public class LoadDataModel
    {
        public List<LookUpp> caseStatuses { get; set; }
        public List<LookUpp> caseTypes { get; set; }
        public List<LookUpp> caseComplexities { get; set; }
        public List<LookUpp> ApprovalStatuses { get; set; }
        //public List<Country> countries { get; set; }
        //public List<Country> dialCodes { get; set; }
        public List<LookUpp> feesTypes { get; set; }
        public List<LookUpp> genders { get; set; }
        public List<LookUpp> profileTypes { get; set; }
        public List<LookUpp> AdditionalCoverages { get; set; }
        public List<LookUpp> userTypes { get; set; }
        public List<LookUpp> customerTypes { get; set; }
        //public List<Benefit> benefits { get; set; }
        //public List<Currency> currencies { get; set; }
        public List<LookUpp> documentTypes { get; set; }
        public List<LookUpp> objectReferences { get; set; }
        public List<Profile> profiles { get; set; }
        public List<Profile> payers { get; set; }
        public List<Profile> partners { get; set; }
        public List<TextReplacement> textReplacements { get; set; }
        public List<LookUpp> departments { get; set; }
        public List<LookUpp> motorCaseTypes { get; set; }
        public List<LookUpp> InsuranceLines { get; set; }
        public List<LookUpp> CallCategories { get; set; }
        //public List<MotorSendEmtailTo> MotorSendEmailTo { get; set; }
        //public List<Location> Regions { get; set; }
        //public List<Location> Locations { get; set; }
        //public List<Location> Cities { get; set; }
        public List<LookUpp> TowingCompanies { get; set; }
        public List<LookUpp> WorkShops { get; set; }
        //public List<ExpertByRegion> Experts { get; set; }
        public List<LookUpp> ExpertsRejectionReasons { get; set; }
        //public List<SysTab> carPlateCodes { get; set; }
        //public List<SysTab> cobs { get; set; }
        public List<LookUpp> policyServices { get; set; }
        public List<LookUpp> towingStatuses { get; set; }
        public List<LookUpp> expertStatuses { get; set; }
        public List<LookUpp> towingTypes { get; set; }
        //public List<FeedbackConfig> feedbackConfigs { get; set; }
        public List<LookUpp> noFeedbackReasons { get; set; }
        public List<LookUpp> positiveFeedbacks { get; set; }
        public List<LookUpp> negativeFeedbacks { get; set; }
        public List<LookUpp> Products { get; set; }
        //public List<Diagnosis> Diagnosis { get; set; }
        public List<LookUpp> GeneralStatus { get; set; }
        //public List<SysTab> LineofBusiness { get; set; }
        //public List<FinancialEmail> financialEmail { get; set; }
    }
}