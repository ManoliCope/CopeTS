using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsersReq
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string User_Name { get; set; }
        public string Category { get; set; }
        public long? Broker_Code { get; set; }
        public int? Country { get; set; }
        public int? Country_Code { get; set; }
        public string? City { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int? Super_Agent_Id { get; set; }
        public string Contact_Person { get; set; }
        public int? Insured_Number { get; set; }
        public double? Tax { get; set; }
        public int? Tax_Type { get; set; }
        public int? Currency { get; set; }
        public int? Rounding_Rule { get; set; }
        public double? Unique_Tax { get; set; }
        public double? Unique_Admin_Tax { get; set; }
        public double? Commission { get; set; }
        public double? Stamp { get; set; }
        public double? Additional_Fees { get; set; }
        public double? VAT { get; set; }
        public bool? For_Syria { get; set; }
        public bool? Show_Commission { get; set; }
        public bool? Fixed_Additional_Fees { get; set; }
        public bool? Apply_Rounding { get; set; }
        public bool? Allow_Cancellation { get; set; }
        public bool? Show_Certificate { get; set; }
        public bool? Cancellation_SubAgent { get; set; }
        public bool? Preview_Total_Only { get; set; }
        public bool? Preview_Net { get; set; }
        public bool? Agents_Creation { get; set; }
        public bool? Agents_Creation_Approval { get; set; }
        public bool? Agents_Commission_ReportView { get; set; }
        public bool? SubAgents_Commission_ReportView { get; set; }
        public bool? Print_Client_Voucher { get; set; }
        public bool? Multi_Lang_Policy { get; set; }
        public bool? Tax_Invoice { get; set; }
        public bool? Hide_Premium_Info { get; set; }
        public bool? Active { get; set; }
        public bool? have_Parents { get; set; }
        public int? Parent_Id { get; set; }
        public double? Max_Additional_Fees { get; set; }
        public DateTime Creation_Date { get; set; }

    }
}
