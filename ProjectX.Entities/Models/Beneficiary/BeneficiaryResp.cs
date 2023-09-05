using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Beneficiary
{
    public class BeneficiaryResp : GlobalResponse
    {
        public int Id { get; set; }
        public int Sex { get; set; }
        public string SexName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Nationalityid { get; set; }
        public int CountryResidenceid { get; set; }
    }
}
