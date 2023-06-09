using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using Utilities;
using ProjectX.Entities.TableTypes;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Notifications;

namespace ProjectX.Repository.NotificationsRepository
{
    public class NotificationsRepository : INotificationsRepository
    {
        private SqlConnection _db;
        private readonly CcAppSettings _appSettings;
        public NotificationsRepository(IOptions<CcAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public NotifResp CreateNewNote(NotifResp req)
        {
            var resp = new NotifResp();
            int statusCode = 0;
            int idOut = 0;
            var param = new DynamicParameters();
            param.Add("@title", req.Title);
            param.Add("@text", req.Text);
            param.Add("@createdby", req.CreatedBy);
            param.Add("@expirydate", req.ExpiryDate);
            param.Add("@expirytime", req.ExpiryTime);
            param.Add("@isImportant", req.isImportant);
            param.Add("@isActive", req.isActive);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            param.Add("@idOut", 0, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);


            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("tr_notif_create_new_notifications", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
                idOut = param.Get<int>("@idOut");
            }
            resp.statusCode.code = statusCode;
            resp.Id = idOut;
            return resp;
        }
        public int DeleteNote(int id,string name)
        {
            int statusCode = 0;
            var param = new DynamicParameters();
            param.Add("@id", id);
            param.Add("@name", name);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("tr_notif_delete_notification", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
            }
            return statusCode;
        }
        public int UpdateNote(int id,string name)
        {
            int statusCode = 0;
            var param = new DynamicParameters();
            param.Add("@id", id);
            param.Add("@name", name);
            param.Add("@Status", statusCode, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("tr_notif_update_notification", param, commandType: CommandType.StoredProcedure);
                statusCode = param.Get<int>("@Status");
            }
            return statusCode;
        }
        public List<NotifResp> GetAllNotes()
        {
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_notif_get_all_notes", commandType: CommandType.StoredProcedure))
                {
                    return result.Read<NotifResp>().ToList();
                }
            }
        }
        public string GetNotificationChyron()
        {
            string chron = "";
           var param = new DynamicParameters();
           param.Add("@chron", chron, dbType: DbType.String, direction: ParameterDirection.InputOutput);
            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                _db.Execute("tr_notif_get_notifications", param, commandType: CommandType.StoredProcedure).ToString();
                chron = param.Get<string>("@chron");
            }
            return chron;
        }
    }
}
