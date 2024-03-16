using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.PrepaidAccounts
{
    public class PreAccSearchResp : GlobalResponse
    {
        public float PA_Amount { get; set; }
        public List<TR_PrepaidAccountsTransactions> preAccTransactions { get; set; }
    }

}
