using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class TR_UsersProduct
    {
        public int UP_Id { get; set; }
        public int? PR_Id { get; set; }
        public string? PR_Name { get; set; }
        public string? UP_UploadedFile { get; set; }
        public int? U_Id { get; set; }
        public double? UP_IssuingFees { get; set; }
        public DateTime? UP_CreationDate { get; set; }
    }
}
