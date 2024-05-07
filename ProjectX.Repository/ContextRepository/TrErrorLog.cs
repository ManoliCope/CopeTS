using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrErrorLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? RequestPath { get; set; }
        public string? Response { get; set; }
        public string? Exception { get; set; }
        public int? ExecutionTime { get; set; }
        public string? UserId { get; set; }
    }
}
