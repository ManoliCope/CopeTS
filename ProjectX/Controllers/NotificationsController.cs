using ProjectX.Business.Notifications;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProjectX.Entities.AppSettings;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace ProjectX.Controllers
{
    public class NotificationsController : Controller
    {
        private User _user;
        private INotificationsBusiness _notificationsBusiness;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NotificationsController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, INotificationsBusiness NotificationsBusiness /*, IGeneralBusiness generalBusiness*/)
        {
            _httpContextAccessor = httpContextAccessor;
            _notificationsBusiness = NotificationsBusiness;
            //_generalBusiness = generalBusiness;
            //_appSettings = appIdentitySettingsAccessor.Value;
            _user = (User)httpContextAccessor.HttpContext.Items["User"];
        }
        public IActionResult Index()
        {
            var notes= new List<NotifResp>();
            notes = _notificationsBusiness.GetAllNotes();
            return View(notes);
        }
        [HttpPost]
        public NotifResp CreateNewNote(NotifResp req)
        {
            req.CreatedBy = _user.UserFullName;
            return _notificationsBusiness.CreateNewNote(req);
        }
        [HttpPost]
        public NotifResp DeleteNote(int Id)
        {
            return _notificationsBusiness.DeleteNote(Id,_user.FirstName);
        }
        [HttpPost]
        public NotifResp UpdateNote(int Id)
        {
             return _notificationsBusiness.UpdateNote(Id, _user.FirstName);
        }
        public string GetNotificationsChyron()
        {
            return _notificationsBusiness.GetNotificationsChyron();
        }
    }
}
