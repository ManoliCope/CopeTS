using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class TR_PrepaidAccounts
    {
        public int PA_Id { get; set; }
        public int U_Id { get; set; }
        public double? PA_Amount { get; set; }
        public bool? PA_Active { get; set; }
        public DateTime? PA_CreationDate { get; set; }
        public DateTime? PA_ModificationDate { get; set; }
    }
}
