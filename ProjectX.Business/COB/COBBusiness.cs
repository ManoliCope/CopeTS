using ProjectX.Repository.COBRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.COB
{
    public class COBBusiness : ICOBBusiness
    {
        ICOBRepository _cobRepository;

        public COBBusiness(ICOBRepository cobRepository)
        {
            _cobRepository = cobRepository;
        }
    }
}
