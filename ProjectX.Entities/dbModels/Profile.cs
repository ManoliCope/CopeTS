using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class Profile
    {
        public string _phoneNumber { get; set; }
        public int IdProfile { get; set; }
        public string Name { get; set; }
        public string IntCode { get; set; }
        public string PhoneNumber
        {
            get
            {
                if (!string.IsNullOrEmpty(_phoneNumber))
                    return string.Concat("+", _phoneNumber);
                else
                    return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }
        public string Email { get; set; }
        public int IdCurrency { get; set; }
        public string Currency { get; set; }
        public int IdFeesType { get; set; }
        public string FeesType { get; set; }
        public decimal? SimpleCaseFees { get; set; }
        public decimal? ComplexCaseFees { get; set; }
        public bool EmailNotificationEnabled { get; set; }
        public bool ApprovalRequired { get; set; }
        public string AccountNo { get; set; }
        public List<LookUpp> countries { get; set; } = new List<LookUpp>();
        public List<ProfileService> profileTypes { get; set; } = new List<ProfileService>();
        //public List<AdditionalCoverage> additionalCoverage { get; set; } = new List<AdditionalCoverage>();
        //public List<Contact> contacts { get; set; }
        public AttachmentModel attachmentModel { get; set; } = new AttachmentModel();
        public string GOP_Template_URL { get; set; }
        public string AllProfileTypes { get; set; }
        public string AllCountries { get; set; }
        public string AllCaseTypes { get; set; }

        public int IdCountry { get; set; }
        public string Country { get; set; }
        public int IdProfileType { get; set; }
        public int IdAdditionalCoverage { get; set; }
        public string ProfileType { get; set; }
        public int IdCaseType { get; set; }
        public string CaseType { get; set; }
        public string HtmlGop { get; set; }

        //public List<ProfileCaseSetup> caseSetups { get; set; } = new List<ProfileCaseSetup>();

        //public string AllProfilesTypes
        //{
        //    get
        //    {
        //        if (profileTypes != null && profileTypes.Count > 0)
        //        {
        //            return string.Join(",", profileTypes.Select(x => x.ProfileType).ToArray());
        //        }
        //        else
        //            return string.Empty;
        //    }
        //}

        //public string AllCountries
        //{
        //    get
        //    {
        //        if (countries != null && countries.Count > 0)
        //        {
        //            return string.Join(",", countries.Select(x => x.LK_TableField).ToArray());

        //        }
        //        else
        //            return string.Empty;
        //    }
        //}

        //public string AllCaseTypes
        //{
        //    get
        //    {
        //        if (profileTypes != null && profileTypes.Count > 0)
        //        {
        //            return string.Join(",", profileTypes.Select(x => x.CaseType).ToArray());
        //        }
        //        else
        //            return string.Empty;
        //    }
        //}
    }
}