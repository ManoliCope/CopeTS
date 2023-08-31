using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsProGetResp : GlobalResponse
    {
        public dbModels.TR_UsersProduct usersproduct { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
