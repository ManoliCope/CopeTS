using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Profile
{
    public class SaveProfileResp : GlobalResponse
    {
        public dbModels.Profile profile { get; set; }
    }
}
