using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class TR_Benefit
    {
        public int B_Id { get; set; }
        public string B_Title { get; set; }
        public double B_Limit { get; set; }
        public int P_Id { get; set; }
        public string P_Name { get; set; }

        public bool B_Is_Plus { get; set; }
        public double B_Benfees { get; set; }

    }
}
