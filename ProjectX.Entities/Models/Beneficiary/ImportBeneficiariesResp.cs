using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Beneficiary
{
	public class ImportBeneficiariesResp
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string PassportNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Nationality { get; set; }
		public string CountryResidence { get; set; }
		public string Gender { get; set; }
		public string RemoveDeductible { get; set; }
		public string AddSportsActivities { get; set; }
		public string Status { get; set; }
		public string Reason { get; set; }
		public string NationalityId { get; set; }
		public string CountryResidenceId { get; set; }
	}
}
