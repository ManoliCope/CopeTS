using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrLog
    {
        public int LoId { get; set; }
        public string? LoMachine { get; set; }
        public string? LoLevel { get; set; }
        public string? LoLogger { get; set; }
        public string? LoMessage { get; set; }
        public string? LoRequestPath { get; set; }
        public string? LoHeaders { get; set; }
        public string? LoDecHeaders { get; set; }
        public string? LoRequestBody { get; set; }
        public string? LoDecRequestBody { get; set; }
        public string? LoResponse { get; set; }
        public string? LoDecResponse { get; set; }
        public string? LoEncKeys { get; set; }
        public string? LoException { get; set; }
        public string? LoStacktrace { get; set; }
        public int? LoExecutionTime { get; set; }
        public string? LoIp { get; set; }
        public string? LoRequestMethod { get; set; }
        public string? LoContentType { get; set; }
        public string? LoBaseLink { get; set; }
        public DateTime LoCreated { get; set; }
        public string? LoUsername { get; set; }
    }
}
