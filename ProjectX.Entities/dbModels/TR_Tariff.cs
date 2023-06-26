using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class TR_Tariff
    {
        public int T_Id { get; set; }
        public int P_Id { get; set; }
        public int T_Start_Age { get; set; }
        public int T_End_Age { get; set; }
        public int T_Number_Of_Days { get; set; }
        public double T_Price_Amount { get; set; }
        public double T_Net_Premium_Amount { get; set; }
        public double T_PA_Amount { get; set; }
        public DateTime T_Tariff_Starting_Date { get; set; }

    }
}
