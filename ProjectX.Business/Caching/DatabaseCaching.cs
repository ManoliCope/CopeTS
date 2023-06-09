using ProjectX.Business.General;
using ProjectX.Entities.dbModels;
using ProjectX.Repository.GeneralRepository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Caching
{
    public class DatabaseCaching : IDatabaseCaching
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IGeneralRepository _generalRepository;

        public DatabaseCaching(IMemoryCache memoryCache, IGeneralRepository generalRepository)
        {
            _memoryCache = memoryCache;
            _generalRepository = generalRepository;
        }

        public IList<AppConfig> GetAppConfigs()
        {
            var cacheEntry = _memoryCache.GetOrCreate("AppConfig", entry =>
            {
                return _generalRepository.GetAppConfigs();
            });
            return cacheEntry;
        }

        public IList<FileDirectory> GetFileDirectories()
        {
            var cacheEntry = _memoryCache.GetOrCreate("FileDirectory", entry =>
            {
                return _generalRepository.GetFileDirectories();
            });
            return cacheEntry;
        }

        public IList<EmailTemplate> GetEmailTemplates()
        {
            var cacheEntry = _memoryCache.GetOrCreate("EmailTemplate", entry =>
            {
                return _generalRepository.GetEmailTemplates();
            });
            return cacheEntry;
        }

        public IList<TextReplacement> GetTextReplacements()
        {
            var cacheEntry = _memoryCache.GetOrCreate("TextReplacement", entry =>
            {
                return _generalRepository.GetTextReplacements();
            });
            return cacheEntry;
        }
    }

}
