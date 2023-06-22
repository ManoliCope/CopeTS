using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsersGetResp : GlobalResponse
    {
        public dbModels.TR_Users users { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
