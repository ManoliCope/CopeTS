using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.BenefitTitle
{
    public class BenTitleSearchResp : GlobalResponse
    {
        public List<TR_BenefitTitle> benefit_title { get; set; }
    }

}
