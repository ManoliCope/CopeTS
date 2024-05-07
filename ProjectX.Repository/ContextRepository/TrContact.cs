using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrContact
    {
        public int CtId { get; set; }
        public int PrId { get; set; }
        public string? CtName { get; set; }
        public string? CtEmail { get; set; }
        public string? CtDialCode { get; set; }
        public string? CtPhone { get; set; }
        public string? CtExtension { get; set; }
        public short? CtIdDepartment { get; set; }
        public short? CtIdCountry { get; set; }
    }
}
