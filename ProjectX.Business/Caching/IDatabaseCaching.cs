using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Caching
{
    public interface IDatabaseCaching
    {
        IList<AppConfig> GetAppConfigs();

        IList<FileDirectory> GetFileDirectories();

        IList<EmailTemplate> GetEmailTemplates();

        IList<TextReplacement> GetTextReplacements();
    }
}
