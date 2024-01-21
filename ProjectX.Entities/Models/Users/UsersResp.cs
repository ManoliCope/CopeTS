using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsersResp : GlobalResponse
    {
        public int id { get; set; }
        public Guid Guid { get; set; }
        public string first_Name { get; set; }
        public string middle_Name { get; set; }
        public string last_Name { get; set; }
        public string user_Name { get; set; }
        public string category { get; set; }
        public long? broker_Code { get; set; }
        public int? country { get; set; }
        public int? country_code { get; set; }
        public string? city { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public int? super_Agent_Id { get; set; }
        public string contact_Person { get; set; }
        public int? insured_Number { get; set; }
        public double? tax { get; set; }
        public int? tax_Type { get; set; }
        public int? currency { get; set; }
        public int? rounding_Rule { get; set; }
        public double? unique_Tax { get; set; }
        public double? unique_Admin_Tax { get; set; }
        public double? commission { get; set; }
        public double? stamp { get; set; }
        public double? retention { get; set; }
        public double? additional_Fees { get; set; }
        public double? vat { get; set; }
        public bool? for_Syria { get; set; }
        public bool? show_Commission { get; set; }
        public bool? fixed_Additional_Fees { get; set; }
        public bool? apply_Rounding { get; set; }
        public bool? allow_Cancellation { get; set; }
        public bool? show_Certificate { get; set; }
        public bool? cancellation_SubAgent { get; set; }
        public bool? preview_Total_Only { get; set; }
        public bool? preview_Net { get; set; }
        public bool? agents_Creation { get; set; }
        public bool? agents_View { get; set; }
        public bool? agents_Creation_Approval { get; set; }
        public bool? agents_Commission_ReportView { get; set; }
        public bool? subAgents_Commission_ReportView { get; set; }
        public bool? print_Client_Voucher { get; set; }
        public bool? multi_Lang_Policy { get; set; }
        public bool? tax_Invoice { get; set; }
        public bool? hide_Premium_Info { get; set; }
        public bool? can_edit { get; set; }
        public bool? can_cancel { get; set; }
        public bool? manual_production { get; set; }
        public bool? active { get; set; }
        public bool? have_Parents { get; set; }
        //public int? parent_Id { get; set; }
        public double? max_Additional_Fees { get; set; }
        public DateTime creation_Date { get; set; }
        public bool? is_Admin { get; set; }
        public string logo { get; set; }
        public string printlayout { get; set; }
        public string signature { get; set; }

    }
}
