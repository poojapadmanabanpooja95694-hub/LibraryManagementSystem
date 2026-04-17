namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string BookCode { get; set; } = string.Empty; // B2001
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;

        public int? Year { get; set; }

        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}