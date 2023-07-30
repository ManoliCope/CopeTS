using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
   public  class GetUserResp : GlobalResponse
    {
        public dbModels.TR_Users user { get; set; }
    }
}
