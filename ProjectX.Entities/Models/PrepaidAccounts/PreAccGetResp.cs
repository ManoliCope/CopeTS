using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.PrepaidAccounts
{
    public class PreAccGetResp : GlobalResponse
    {
        public dbModels.TR_PrepaidAccountsTransactions preAccTransactions { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
