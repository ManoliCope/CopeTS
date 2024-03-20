using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class TR_PrepaidAccountsTransactions
    {
        public int PAT_Id { get; set; }
        public int? PA_Id { get; set; }
        public int? PAT_Action { get; set; }
        public string? PAT_ActionName { get; set; }
        public double? PAT_Amount { get; set; }
        public string? PAT_Details { get; set; }
        public DateTime? PAT_CreationDate { get; set; }
    }
}
