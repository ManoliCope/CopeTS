using ProjectX.Repository.TariffRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Tariff
{
    public class TariffBusiness : ITariffBusiness
    {
        ITariffRepository _tariffRepository;

        public TariffBusiness(ITariffRepository tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }
    }
}
