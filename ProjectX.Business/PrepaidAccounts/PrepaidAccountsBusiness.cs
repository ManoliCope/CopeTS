using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.CurrencyRate;
using ProjectX.Repository.PrepaidAccountsRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.bModels;
using ProjectX.Entities.Models.PrepaidAccounts;

namespace ProjectX.Business.PrepaidAccounts
{
    public class PrepaidAccountsBusiness : IPrepaidAccountsBusiness
    {
        IPrepaidAccountsRepository _prepaidAccountsRepository;
        public PrepaidAccountsBusiness(IPrepaidAccountsRepository prepaidAccountsRepository)
        {
            _prepaidAccountsRepository = prepaidAccountsRepository;
        }
        public LoadDataModel GetAvailableUsers(int userid)
        {
            return _prepaidAccountsRepository.GetAvailableUsers(userid);
        }
        public PreAccSearchResp GetUserBalance(int userid)
        {
            return _prepaidAccountsRepository.GetUserBalance(userid);
        }
        public PreAccResp EditBalance(int createdBy,int action,float amount, int userid)
        {
            return _prepaidAccountsRepository.EditBalance(createdBy, action, amount,userid);
        }


    }
}
