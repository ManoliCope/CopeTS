using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsersSearchResp : GlobalResponse
    {
        public string parent_name { get; set; }
        public List<TR_Users> users { get; set; }
    }

}
