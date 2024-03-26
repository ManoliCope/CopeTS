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
using ProjectX.Entities.Models.Beneficiary;
using System.Reflection;

namespace ProjectX.Repository.BeneficiaryRepository
{
	public class BeneficiaryRepository : IBeneficiaryRepository
	{
		private SqlConnection _db;
		private readonly TrAppSettings _appSettings;

		public BeneficiaryRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
		{
			_appSettings = appIdentitySettingsAccessor.Value;
		}
		public BeneficiaryResp ModifyBeneficiary(BeneficiaryReq req, string act, int userid)
		{
			var resp = new BeneficiaryResp();
			int statusCode = 0;
			int idOut = 0;
			var param = new DynamicParameters();
			param.Add("@action", act);
			param.Add("@user_id", userid);
			param.Add("@BE_Id", req.Id);
			param.Add("@BE_Sex", req.Sex);
			param.Add("@BE_FirstName", req.FirstName);
			param.Add("@BE_MiddleName", req.MiddleName);
			param.Add("@BE_LastName", req.LastName);
			param.Add("@BE_MaidenName", req.LastName);
			param.Add("@BE_PassportNumber", req.PassportNumber);
			param.Add("@BE_DOB", req.DateOfBirth);
			param.Add("@BE_CountryResidenceid", req.CountryResidenceid);
			param.Add("@BE_Nationalityid", req.Nationalityid);
			param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
			param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


			using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
			{
				_db.Execute("TR_Beneficiary_CRUD", param, commandType: CommandType.StoredProcedure);
				statusCode = param.Get<int>("@Status");
				idOut = param.Get<int>("@Returned_ID");
			}
			resp.statusCode.code = statusCode;
			resp.Id = idOut;
			return resp;
		}
		public List<TR_Beneficiary> GetBeneficiaryList(BeneficiarySearchReq req, int userid)
		{
			var resp = new List<TR_Beneficiary>();

			var param = new DynamicParameters();

			param.Add("@BE_Id", req.Id);
			param.Add("@BE_Sex", req.Id);
			param.Add("@BE_FirstName", req.FirstName);
			param.Add("@BE_MiddleName", req.MiddleName);
			param.Add("@BE_LastName", req.LastName);
			param.Add("@BE_MaidenName", req.MaidenName);
			param.Add("@BE_PassportNumber", req.PassportNumber);
			param.Add("@BE_DOB", req.DateOfBirth);
			param.Add("@user_id", userid);



			using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
			{

				using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Beneficiary_Get", param, commandType: CommandType.StoredProcedure))
				{
					resp = result.Read<TR_Beneficiary>().ToList();

				}
			}

			return resp;
		}
		public TR_Beneficiary GetBeneficiary(int IdBeneficiary, int userid)
		{
			var resp = new TR_Beneficiary();
			var param = new DynamicParameters();
			param.Add("@BE_Id", IdBeneficiary);
			param.Add("@user_id", userid);

			using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
			{
				using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Beneficiary_GetbyID", param, commandType: CommandType.StoredProcedure))
				{
					resp = result.ReadFirstOrDefault<TR_Beneficiary>();
				}
			}

			return resp;
		}

		public BeneficiarySearchResp SearchBeneficiaryPref(string prefix, int userid)
		{
			var resp = new BeneficiarySearchResp();
			var param = new DynamicParameters();
			param.Add("@BE_Prefix", prefix);
			param.Add("@user_id", userid);

			using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
			{
				using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Beneficiary_GetbyPerfix", param, commandType: CommandType.StoredProcedure))
				{
					resp.beneficiary = result.Read<TR_Beneficiary>().ToList();
				}
			}

			return resp;
		}

		public BeneficiariesBatchSaveResp SaveBeneficiariesBatch(BeneficiariesBatchSaveReq req, int isProduction)
		{
			int statusCode = 0;
			var resp = new BeneficiariesBatchSaveResp();
			var param = new DynamicParameters();
			var batches = ConvertToDataTable(req.beneficiaries);

			param.Add("@userid", req.userid);
			param.Add("@userid", req.userid);
			param.Add("@BenefList", batches.AsTableValuedParameter("TR_ImportBeneficiaries_Req"));
			param.Add("@isProduction", isProduction);
			param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

			using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
			{
				using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
				{
					using (SqlMapper.GridReader result = _db.QueryMultiple("TR_ImportBeneficiaries", param, commandType: CommandType.StoredProcedure))
					{
						resp.beneficiaries = result.Read<ImportBeneficiariesResp>().ToList();
						statusCode = param.Get<int>("@Status");
					}
					resp.statusCode.code = statusCode;
				}
			}

			return resp;
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
