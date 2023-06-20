using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.Models.Notifications;
using ProjectX.Entities.Resources;
using ProjectX.Repository.NotificationsRepository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace ProjectX.Business.Notifications
{
    public class NotificationsBusiness : INotificationsBusiness
    {
        INotificationsRepository _notificationsRepository;
        public NotificationsBusiness(IOptions<TrAppSettings> appIdentitySettingsAccessor, INotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }
        public NotifResp CreateNewNote(NotifResp req)
        {
            var resp = new NotifResp();
            resp = _notificationsRepository.CreateNewNote(req);
            resp.statusCode = ResourcesManager.getStatusCode(Languages.english, (StatusCodeValues)resp.statusCode.code, SuccessCodeValues.Update, "Notification");
            return resp;
        }
        public List<NotifResp> GetAllNotes()
        {
            return _notificationsRepository.GetAllNotes();
        }
        public NotifResp DeleteNote(int Id,string name)
        {
            int code = 0;
            var resp = new NotifResp();

            code= _notificationsRepository.DeleteNote(Id,name);
            resp.statusCode = ResourcesManager.getStatusCode(Languages.english, (StatusCodeValues)code, SuccessCodeValues.Update, "Notification");
            return resp;
        }
        public NotifResp UpdateNote(int Id, string name)
        {
            int code = 0;
            var resp = new NotifResp();
            code = _notificationsRepository.UpdateNote(Id,name);
            resp.statusCode = ResourcesManager.getStatusCode(Languages.english, (StatusCodeValues)code, SuccessCodeValues.Update, "Notification");
            return resp;
        }
        public string GetNotificationsChyron()
        {
           return _notificationsRepository.GetNotificationChyron();
        }
    }
}
