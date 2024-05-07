using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPageBackup
    {
        public int PgId { get; set; }
        public int PgParent { get; set; }
        public string PgName { get; set; } = null!;
        public string? PgDesc { get; set; }
        public string? PgUrl { get; set; }
        public string? PgUrlParam { get; set; }
        public bool PgIsAspx { get; set; }
        public int PgSort { get; set; }
        public string? PgLogo { get; set; }
        public DateTime PgCreated { get; set; }
        public bool PgIsDefault { get; set; }
        public bool PgAllowInMenu { get; set; }
        public bool PgAllowView { get; set; }
    }
}
