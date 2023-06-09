using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class Page
    {
        public int PG_ID { get; set; }
        public int PG_Parent { get; set; }
        public string PG_Name { get; set; }
        public string PG_Desc { get; set; }
        public string PG_Url { get; set; }
        public bool GP_AllowView { get; set; }
        public bool GP_AllowInsert { get; set; }
        public bool GP_AllowUpdate { get; set; }
        public bool GP_AllowDelete { get; set; }
        public bool GP_AllowPrint { get; set; }
        public bool GP_AllowExecute { get; set; }
        public bool GP_AllowCopy { get; set; }
        public bool GP_AllowRenew { get; set; }
        public bool GP_AllowInMenu { get; set; }
        public bool PG_IsDefault { get; set; }
        public string PG_Logo { get; set; }
        public bool PG_IsAspx { get; set; }
        public string PG_UrlParam { get; set; }
        public bool GP_AllowAudit { get; set; }
    }
}