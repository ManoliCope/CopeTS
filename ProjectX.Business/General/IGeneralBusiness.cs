using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.General
{
    public interface IGeneralBusiness
    {
        LoadDataResp loadData(LoadDataModelSetup loadDataModelSetup);
    }
}
