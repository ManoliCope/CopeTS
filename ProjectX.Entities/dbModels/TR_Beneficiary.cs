using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class TR_Beneficiary
    {
        public int BE_Id { get; set; }
        public int BE_Sex { get; set; }
        public string BE_SexName { get; set; }
        public string BE_FirstName { get; set; }
        public string BE_MiddleName { get; set; }
        public string BE_LastName { get; set; }
        public string BE_PassportNumber { get; set; }
        public string BE_MaidenName { get; set; }
        public DateTime? BE_DOB { get; set; }
        public int BE_Nationalityid { get; set; }
        public int BE_CountryResidenceid { get; set; }
        public string BE_CreatedbyUser { get; set; }
    }


}
