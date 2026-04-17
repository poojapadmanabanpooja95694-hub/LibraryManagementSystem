using LibraryManagementSystem.Models;

public class Issue
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public int BookId { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }

    public string Status { get; set; } = "Issued";

    public Student Student { get; set; } = null!;
    public Book Book { get; set; } = null!;
}