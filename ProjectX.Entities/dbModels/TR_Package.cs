using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class TR_Package
    {
        public int P_Id { get; set; }
        public string P_Name { get; set; }
        public int PR_Id { get; set; }
        public int P_ZoneID { get; set; }
        public int P_Remove_deductable { get; set; }
        public int P_Adult_No { get; set; }
        public int P_Children_No { get; set; }
        public bool P_PA_Included { get; set; }

    }
}
