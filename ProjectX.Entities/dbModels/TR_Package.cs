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
        public string PR_Name { get; set; }
        public int P_ZoneID { get; set; }
        public string ZN_Name { get; set; }

        public int P_Remove_deductable { get; set; }
        public int P_Adult_No { get; set; }
        public int P_Children_No { get; set; }
        public bool P_PA_Included { get; set; }
        public int P_Adult_Max_Age { get; set; }
        public int P_Child_Max_Age { get; set; }
        public bool P_Special_Case { get; set; }

    }
}
