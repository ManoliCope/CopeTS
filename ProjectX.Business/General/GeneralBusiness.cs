using ProjectX.Business.Caching;
using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Resources;
using ProjectX.Repository.GeneralRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectX.Business.General
{
    public class GeneralBusiness : IGeneralBusiness
    {
        IGeneralRepository _generalRepository;
        private IList<AppConfig> _appConfigs;
        private IDatabaseCaching _databaseCaching;

        public GeneralBusiness(IGeneralRepository generalRepository, IDatabaseCaching databaseCaching)
        {
            _generalRepository = generalRepository;
            _databaseCaching = databaseCaching;
            _appConfigs = _databaseCaching.GetAppConfigs();
        }

        public LoadDataResp loadData(LoadDataModelSetup loadDataModelSetup)
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = _generalRepository.loadData(loadDataModelSetup);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            return response;
        }
    }
}
