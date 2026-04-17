namespace LibraryManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string StudentCode { get; set; } = string.Empty; // S1001
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}