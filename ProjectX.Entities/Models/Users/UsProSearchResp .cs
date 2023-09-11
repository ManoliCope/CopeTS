using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsProSearchResp : GlobalResponse
    {
        public List<TR_UsersProduct> usersproduct { get; set; }
        public string Directory { get; set; }
    }

}
