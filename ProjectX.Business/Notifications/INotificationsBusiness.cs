using ProjectX.Entities.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Notifications
{
    public interface INotificationsBusiness
    {
        NotifResp CreateNewNote(NotifResp req);
        public List<NotifResp> GetAllNotes();
        public NotifResp DeleteNote(int Id, string name);
        public NotifResp UpdateNote(int Id, string name);
        public string GetNotificationsChyron();
    }
}
