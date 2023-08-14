using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.Models.Users
{
    public class UserRights
    {
        public bool? Fixed_Additional_Fees { get; set; }
        public bool? Allow_Cancellation { get; set; }
        public bool? Cancellation_SubAgent { get; set; }
        public bool? Preview_Total_Only { get; set; }
        public bool? Preview_Net { get; set; }
        public bool? Agents_Creation { get; set; }
        public bool? Agents_Commission_ReportView { get; set; }
        public bool? SubAgents_Commission_ReportView { get; set; }
        public bool? Multi_Lang_Policy { get; set; }
        public bool? Hide_Premium_Info { get; set; }
        public bool? Active { get; set; }
        public bool? Is_Admin { get; set; }
        public double? Commission { get; set; }
        public double? Stamp { get; set; }
        public double? Addional_Fees { get; set; }
        public double? Max_Additional_Fees { get; set; }
        public double? VAT { get; set; }
        public double? Tax { get; set; }
        public int? Tax_Type { get; set; }

    }
}
