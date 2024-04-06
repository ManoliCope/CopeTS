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
using System.Reflection;

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
        public TariffResp ImportDataTariff(List<TR_Tariff> tariffs, int userid)
        {
            DataTable ListTariff = ConvertToDataTable(tariffs);
            var resp = new TariffResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@TariffList", ListTariff.AsTableValuedParameter("TR_Tariff"));
            //param.Add("IdList", ListTariff, DbType.Object, ParameterDirection.Input);
            param.Add("@userid", userid);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

            try
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    _db.Execute("TR_Tariff_Import", param, commandType: CommandType.StoredProcedure);
                    statusCode = param.Get<int>("@Status");
                    idOut = param.Get<int>("@Returned_ID");
                }
                resp.statusCode.code = statusCode;

                if(statusCode == 0)
                    resp.statusCode.message = "Tariff already exists";

                resp.id = idOut;
                return resp;
            }
            catch (SqlException ex)
            {

                if (ex.Number == 2627)
                    resp.statusCode.message = "Tariff already exists";
                else
                    resp.statusCode.message = "Error Try Again";


                resp.statusCode.code = 0;
                resp.id = 0;

                return resp;
            }

        }
        public static DataTable ConvertToDataTable<T>(IEnumerable<T> list)
        {
            DataTable dataTable = new DataTable();

            // Get all the public properties of the class using reflection
            PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Create data columns for the DataTable using the property names and types
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                dataTable.Columns.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }

            // Add data rows to the DataTable
            foreach (T item in list)
            {
                DataRow dataRow = dataTable.NewRow();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(item);
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}
