using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.GeneralRepository
{
    public interface IGeneralRepository
    {
        LoadDataModel loadData(LoadDataModelSetup loadDataModelSetup);

        IList<AppConfig> GetAppConfigs();

        IList<FileDirectory> GetFileDirectories();

        IList<EmailTemplate> GetEmailTemplates();

        IList<TextReplacement> GetTextReplacements();
    }
}
