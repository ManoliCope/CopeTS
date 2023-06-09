using ProjectX.Entities.dbModels;
using ProjectX.Entities.TableTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Profile
{
    public class SaveProfileReq
    {
        public int IdProfile { get; set; }
        public string Name { get; set; }
        public int IdProfileCountry { get; set; }
        public string IntCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int IdCurrency { get; set; }
        //public int IdFeesType { get; set; }
        //public decimal? SimpleCaseAmount { get; set; }
        //public decimal? ComplexCaseAmount { get; set; }
        public bool EmailNotificationEnabled { get; set; }
        public bool ApprovalRequired { get; set; }
        public string AccountNo { get; set; }
        //public List<Contact> contacts { get; set; }
        public List<int> countries { get; set; }
        public List<int> profileTypes { get; set; }
        public List<int> additionalCoverage { get; set; }
        //public List<ProfileCaseSetup> caseSetups { get; set; }

    }
}
