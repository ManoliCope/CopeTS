﻿using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Benefit;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.BenefitRepository
{
    public interface IBenefitRepository
    {
        public BenResp ModifyBenefit(BenResp req);
        public List<TR_Benefit> GetBenefitList(BenReq req);
        public TR_Benefit GetBenefit(int Idproduct);
    }
}
