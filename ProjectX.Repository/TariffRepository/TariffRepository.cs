using ProjectX.Entities.AppSettings;
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
using ProjectX.Entities.Models.Tariff;
using OfficeOpenXml;

namespace ProjectX.Repository.TariffRepository
{
    public class TariffRepository : ITariffRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public TariffRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public TariffResp ModifyTariff(TariffReq req, string act, int userid)
        {
            var resp = new TariffResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@T_Id", req.id);
            param.Add("@P_Id", req.idPackage);
            param.Add("@T_Start_Age", req.start_age);
            param.Add("@T_End_Age", req.end_age);
            param.Add("@T_Number_Of_Days", req.number_of_days);
            param.Add("@T_Price_Amount", req.price_amount);
            param.Add("@T_Net_Premium_Amount", req.net_premium_amount);
            param.Add("@T_PA_Amount", req.pa_amount);
            param.Add("@T_Tariff_Starting_Date", req.tariff_starting_date);
            param.Add("@T_Override_Amount", req.Override_Amount);
            param.Add("@PL_Id", req.planId);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            //test
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Tariff_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.id = idOut;
            return resp;
        }
        public List<TR_Tariff> GetTariffList(TariffSearchReq req)
        {
            var resp = new List<TR_Tariff>();
            
            var param = new DynamicParameters();
            param.Add("@T_Id", req.id);
            param.Add("@P_Id", req.idPackage);
            param.Add("@T_Start_Age", req.start_age);
            param.Add("@T_End_Age", req.end_age);
            param.Add("@T_Number_Of_Days", req.number_of_days);
            param.Add("@T_Price_Amount", req.price_amount);
            param.Add("@T_Net_Premium_Amount", req.net_premium_amount);
            param.Add("@T_PA_Amount", req.pa_amount);
            param.Add("@T_Override_Amount", req.Override_Amount);
            param.Add("@PL_Id", req.planId);
            //   param.Add("@T_Tariff_Starting_Date", req.tariff_starting_date);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Tariff_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Tariff>().ToList();
                 
                }
               
               
            }
            return resp;
        }
        public TR_Tariff GetTariff(int IdTariff)
        {
            var resp = new TR_Tariff();
            var param = new DynamicParameters();
            param.Add("@T_id", IdTariff);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Tariff_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_Tariff>();
                }
            }

            return resp;
        }
        public TariffResp Import(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);


                using (var package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    //using (var dbContext = new YourDbContext()) // Replace with your actual DbContext class
                    //{
                    for (int row = 2; row <= rowCount; row++) // Assuming header is in the first row
                    {
                        string columnName = worksheet.Cells[row, 1].Value.ToString(); // Replace with actual column index
                                                                                      // ... Extract other columns ...

                        //YourEntity entity = new YourEntity
                        //{
                        //    ColumnName = columnName,
                        //    // ... Set other properties ...
                        //};

                        //dbContext.YourEntities.Add(entity);
                    }

                    //dbContext.SaveChanges();
                    //}
                }

               
            }
            catch (Exception ex)
            {
               
            }
            return null;
           
        }
    }
}
