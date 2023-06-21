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
using ProjectX.Entities.Models.Users;

namespace ProjectX.Repository.UsersRepository
{
    public class UsersRepository : IUsersRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;

        public UsersRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public UsersResp ModifyUser(UsersReq req, string act, int userid)
        {
            var resp = new UsersResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@action", act);
            param.Add("@user_id", userid);
            param.Add("@U_Id", req.Id);
            param.Add("@U_First_Name", req.First_Name);
            param.Add("@U_Middle_Name", req.Middle_Name);
            param.Add("@U_Last_Name", req.Last_Name);
            param.Add("@U_Category", req.Category);
            param.Add("@U_Broker_Code", req.Broker_Code);
            param.Add("@U_Country", req.Country);
            param.Add("@U_City", req.City);
            param.Add("@U_Telephone", req.Telephone);
            param.Add("@U_Email", req.Email);
            param.Add("@U_Super_Agent_Id", req.Super_Agent_Id);
            param.Add("@U_Contact_Person", req.Contact_Person);
            param.Add("@U_Insured_Number", req.Insured_Number);
            param.Add("@U_Tax", req.Tax);
            param.Add("@U_Tax_Type", req.Tax_Type);
            param.Add("@U_Currency", req.Currency);
            param.Add("@U_Rounding_Rule", req.Rounding_Rule);
            param.Add("@U_Unique_Tax", req.Unique_Tax);
            param.Add("@U_Unique_Admin_Tax", req.Unique_Admin_Tax);
            param.Add("@U_Commission", req.Commission);
            param.Add("@U_Stamp", req.Stamp);
            param.Add("@U_Additional_Fees", req.Additional_Fees);
            param.Add("@U_VAT", req.VAT);
            param.Add("@U_For_Syria", req.For_Syria);
            param.Add("@U_Show_Commission", req.Show_Commission);
            param.Add("@U_Fixed_Additional_Fees", req.Fixed_Additional_Fees);
            param.Add("@U_Apply_Rounding", req.Apply_Rounding);
            param.Add("@U_Allow_Cancellation", req.Allow_Cancellation);
            param.Add("@U_Show_Certificate", req.Show_Certificate);
            param.Add("@U_Cancellation_SubAgent", req.Cancellation_SubAgent);
            param.Add("@U_Preview_Total_Only", req.Preview_Total_Only);
            param.Add("@U_Preview_Net", req.Preview_Net);
            param.Add("@U_Agents_Creation", req.Agents_Creation);
            param.Add("@U_Agents_Creation_Approval", req.Agents_Creation_Approval);
            param.Add("@U_Agents_Commission_ReportView", req.Agents_Commission_ReportView);
            param.Add("@U_SubAgents_Commission_ReportView", req.SubAgents_Commission_ReportView);
            param.Add("@U_Print_Client_Voucher", req.Print_Client_Voucher);
            param.Add("@U_Multi_Lang_Policy", req.Multi_Lang_Policy);
            param.Add("@U_Tax_Invoice", req.Tax_Invoice);
            param.Add("@U_Hide_Premium_Info", req.Hide_Premium_Info);
            param.Add("@U_Active", req.Active);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@Returned_ID", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);




            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("TR_Users_CRUD", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@Returned_ID");
            }
            resp.statusCode.code = statusCode;
            resp.Id = idOut;
            return resp;
        }
        public List<TR_Users> GetUsersList(UsersSearchReq req)
        {
            var resp = new List<TR_Users>();

            var param = new DynamicParameters();

            param.Add("@U_Id", req.Id);
            param.Add("@U_First_Name", req.First_Name);
            param.Add("@U_Middle_Name", req.Middle_Name);
            param.Add("@U_Last_Name", req.Last_Name);
            param.Add("@U_Category", req.Category);
            param.Add("@U_Broker_Code", req.Broker_Code);
            param.Add("@U_Country", req.Country);
            param.Add("@U_City", req.City);
            param.Add("@U_Telephone", req.Telephone);
            param.Add("@U_Email", req.Email);
            param.Add("@U_Super_Agent_Id", req.Super_Agent_Id);
            param.Add("@U_Contact_Person", req.Contact_Person);
            param.Add("@U_Insured_Number", req.Insured_Number);
            param.Add("@U_Tax", req.Tax);
            param.Add("@U_Tax_Type", req.Tax_Type);
            param.Add("@U_Currency", req.Currency);
            param.Add("@U_Rounding_Rule", req.Rounding_Rule);
            param.Add("@U_Unique_Tax", req.Unique_Tax);
            param.Add("@U_Unique_Admin_Tax", req.Unique_Admin_Tax);
            param.Add("@U_Commission", req.Commission);
            param.Add("@U_Stamp", req.Stamp);
            param.Add("@U_Additional_Fees", req.Additional_Fees);
            param.Add("@U_VAT", req.VAT);
            param.Add("@U_For_Syria", req.For_Syria);
            param.Add("@U_Show_Commission", req.Show_Commission);
            param.Add("@U_Fixed_Additional_Fees", req.Fixed_Additional_Fees);
            param.Add("@U_Apply_Rounding", req.Apply_Rounding);
            param.Add("@U_Allow_Cancellation", req.Allow_Cancellation);
            param.Add("@U_Show_Certificate", req.Show_Certificate);
            param.Add("@U_Cancellation_SubAgent", req.Cancellation_SubAgent);
            param.Add("@U_Preview_Total_Only", req.Preview_Total_Only);
            param.Add("@U_Preview_Net", req.Preview_Net);
            param.Add("@U_Agents_Creation", req.Agents_Creation);
            param.Add("@U_Agents_Creation_Approval", req.Agents_Creation_Approval);
            param.Add("@U_Agents_Commission_ReportView", req.Agents_Commission_ReportView);
            param.Add("@U_SubAgents_Commission_ReportView", req.SubAgents_Commission_ReportView);
            param.Add("@U_Print_Client_Voucher", req.Print_Client_Voucher);
            param.Add("@U_Multi_Lang_Policy", req.Multi_Lang_Policy);
            param.Add("@U_Tax_Invoice", req.Tax_Invoice);
            param.Add("@U_Hide_Premium_Info", req.Hide_Premium_Info);
            param.Add("@U_Active", req.Active);



            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {

                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Users_Get", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.Read<TR_Users>().ToList();

                }
            }

            return resp;
        }
        public TR_Users GetUser(int IdUser)
        {
            var resp = new TR_Users();
            var param = new DynamicParameters();
            param.Add("@U_ID", IdUser);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("TR_Users_GetbyID", param, commandType: CommandType.StoredProcedure))
                {
                    resp = result.ReadFirstOrDefault<TR_Users>();
                }
            }

            return resp;
        }
    }
}
