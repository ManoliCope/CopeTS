using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Notifications
{
    public class NotifResp : GlobalResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
       public string Text { get; set; }
       public string CreatedBy { get; set; }
       public DateTime CreationDate { get; set; }
       public string ExpiryDate { get; set; }
       public string ExpiryTime { get; set; }
       public bool isActive { get; set; }
       public bool isDeleted { get; set; }
       public bool isImportant { get; set; }


    }
}
