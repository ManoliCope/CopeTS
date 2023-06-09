using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Profile
{
    public class SaveGopTemplateReq
    {
        public int idProfile { get; set; }
        public int idDocument { get; set; }
        public string gopHtmlText { get; set; }
    }
}
