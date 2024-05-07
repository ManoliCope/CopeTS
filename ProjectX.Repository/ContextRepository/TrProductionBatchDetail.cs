using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrProductionBatchDetail
    {
        public int Id { get; set; }
        public int? BatchId { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? Type { get; set; }
        public string? Plan { get; set; }
        public string? Zone { get; set; }
        public int? Days { get; set; }
        public DateTime? StartDate { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? PassportNumber { get; set; }
        public string? Nationality { get; set; }
        public decimal? PremiumInUsd { get; set; }
        public decimal? NetInUsd { get; set; }
        public int? BenId { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public string? Country { get; set; }
        public string? Deductible { get; set; }
        public string? SportsActivities { get; set; }
        public string? Product { get; set; }
    }
}
