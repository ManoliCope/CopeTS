using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class LookUpp
    {
        public int LK_ID { get; set; }
        public int LK_ParentID { get; set; }
        public string LK_ParentName { get; set; }
        public string LK_TableField { get; set; }
        public bool LK_IsActive { get; set; }        
        public string LK_Icon { get; set; }        
    }
}
