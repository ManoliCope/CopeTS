using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class TR_Users
    {
        public int U_Id { get; set; }
        public string U_First_Name { get; set; }
        public string U_Middle_Name { get; set; }
        public string U_Last_Name { get; set; }
        public string U_Category { get; set; }
        public long? U_Broker_Code { get; set; }
        public int? U_Country { get; set; }
        public int? U_Country_Code { get; set; }
        public int? U_City { get; set; }
        public string? U_City_Name { get; set; }
        public string U_Telephone { get; set; }
        public string U_Email { get; set; }
        public int? U_Super_Agent_Id { get; set; }
        public string U_Contact_Person { get; set; }
        public int? U_Insured_Number { get; set; }
        public double? U_Tax { get; set; }
        public int? U_Tax_Type { get; set; }
        public int? U_Currency { get; set; }
        public int? U_Rounding_Rule { get; set; }
        public double? U_Unique_Tax { get; set; }
        public double? U_Unique_Admin_Tax { get; set; }
        public double? U_Commission { get; set; }
        public double? U_Stamp { get; set; }
        public double? U_Additional_Fees { get; set; }
        public double? U_VAT { get; set; }
        public double? U_Max_Additional_Fees { get; set; }
        public DateTime U_Creation_Date { get; set; }
        public int U_Tax_Format { get; set; }

        public bool? U_For_Syria { get; set; }
        public bool? U_Show_Commission { get; set; }
        public bool? U_Fixed_Additional_Fees { get; set; }
        public bool? U_Apply_Rounding { get; set; }
        public bool? U_Allow_Cancellation { get; set; }
        public bool? U_Show_Certificate { get; set; }
        public bool? U_Cancellation_SubAgent { get; set; }
        public bool? U_Preview_Total_Only { get; set; }
        public bool? U_Preview_Net { get; set; }
        public bool? U_Agents_Creation { get; set; }
        public bool? U_Agents_Creation_Approval { get; set; }
        public bool? U_Agents_Commission_ReportView { get; set; }
        public bool? U_SubAgents_Commission_ReportView { get; set; }
        public bool? U_Print_Client_Voucher { get; set; }
        public bool? U_Multi_Lang_Policy { get; set; }
        public bool? U_Tax_Invoice { get; set; }
        public bool? U_Hide_Premium_Info { get; set; }
        public bool? U_Active { get; set; }
        public bool? U_Have_Parents { get; set; }
        public int? U_Parent_Id { get; set; }
        public string? U_Full_Name { get; set; }
        public string? U_User_Name { get; set; }
        public bool? U_Is_Admin { get; set; }
        public decimal? U_CurrencyRate { get; set; }
        public string U_CurrencySymbol { get; set; }

    }
}