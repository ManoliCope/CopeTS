using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrFileDirectory
    {
        public int FdId { get; set; }
        public byte? FdIdObjectReference { get; set; }
        public string? FdName { get; set; }
        public string? FdNameMd5 { get; set; }
        public string? FdPath { get; set; }
        public string? FdDescription { get; set; }
        public byte FdIdFileType { get; set; }
        public bool FdAllowDowload { get; set; }
    }
}
