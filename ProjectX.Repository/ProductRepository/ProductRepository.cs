﻿using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ProjectX.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private SqlConnection _db;
        private readonly CcAppSettings _appSettings;

        public ProductRepository(IOptions<CcAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }


    }
}
