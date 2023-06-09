using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Profile
{
    public class GetProfileResp : GlobalResponse
    {
        public dbModels.Profile profile { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
