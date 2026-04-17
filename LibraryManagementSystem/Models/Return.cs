using System;

namespace LibraryManagementSystem.Models
{
    public class Return
    {
        public string ReturnId { get; set; } = null!;   // Primary Key

        public int IssueId { get; set; }               // Foreign Key

        public DateTime ReturnDate { get; set; }

        public decimal FineAmount { get; set; }

        public string? Remarks { get; set; }           // Optional

        // Navigation Property
        public Issue Issue { get; set; } = null!;
    }
}