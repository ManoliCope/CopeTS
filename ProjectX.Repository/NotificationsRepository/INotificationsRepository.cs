using ProjectX.Entities.Models.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.NotificationsRepository
{
    public interface INotificationsRepository
    {
        public NotifResp CreateNewNote(NotifResp req);
        public List<NotifResp> GetAllNotes();
        public int DeleteNote(int id,string name);
        public int UpdateNote(int id, string name);
        public string GetNotificationChyron();
    }
}
