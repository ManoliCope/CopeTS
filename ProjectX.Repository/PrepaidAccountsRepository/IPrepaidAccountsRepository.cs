using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.CurrencyRate;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.PrepaidAccounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.PrepaidAccountsRepository
{
    public interface IPrepaidAccountsRepository
    {
         LoadDataModel GetAvailableUsers(int userid);
         PreAccSearchResp GetUserBalance(int userid);
        PreAccResp EditBalance(int createdBy, int action, float amount, int userid);
    }
}
