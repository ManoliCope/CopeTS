using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.PrepaidAccounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.PrepaidAccounts
{
    public interface IPrepaidAccountsBusiness
    {
        public LoadDataModel GetAvailableUsers(int userid);
        public PreAccSearchResp GetUserBalance(int userid);
        public PreAccResp EditBalance(int createdBy, int action, float amount, int userid);

    }
}
