using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using System;
using System.Linq;

namespace LibraryManagementSystem.Services
{
    public class IssueService
    {
        // ✅ ISSUE BOOK
        public void IssueBookByCode(string studentCode, string bookCode)
        {
            using var context = new LibraryContext();

            var student = context.Students
                .FirstOrDefault(s => s.StudentCode == studentCode && s.IsActive);

            if (student == null)
            {
                Console.WriteLine("Student not found!");
                return;
            }

            var book = context.Books
                .FirstOrDefault(b => b.BookCode == bookCode && b.IsActive);

            if (book == null)
            {
                Console.WriteLine("Book not found!");
                return;
            }

            if (book.AvailableCopies <= 0)
            {
                Console.WriteLine("No copies available!");
                return;
            }

            // 🔥 MAX BOOK LIMIT
            int maxBooks = 3;
            var activeCount = context.Issues
                .Count(i => i.StudentId == student.Id && i.Status == "Issued");

            if (activeCount >= maxBooks)
            {
                Console.WriteLine("Student reached max book limit!");
                return;
            }

            var issue = new Issue
            {
                StudentId = student.Id,
                BookId = book.Id,
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Status = "Issued"
            };

            book.AvailableCopies--;

            context.Issues.Add(issue);
            context.SaveChanges();

            Console.WriteLine($"Book issued successfully! Issue Id: {issue.Id}");
        }

        // ✅ LIST ALL ISSUES (FULL DETAILS)
        public void ListAllIssues()
        {
            using var context = new LibraryContext();

            var issues = context.Issues
                .Select(i => new
                {
                    i.Id,
                    Student = i.Student.FullName,
                    Book = i.Book.Title,
                    i.IssueDate,
                    i.DueDate,
                    i.Status
                })
                .ToList();

            if (!issues.Any())
            {
                Console.WriteLine("No issues found.");
                return;
            }

            foreach (var i in issues)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Issue Id   : {i.Id}");
                Console.WriteLine($"Student    : {i.Student}");
                Console.WriteLine($"Book       : {i.Book}");
                Console.WriteLine($"Issue Date : {i.IssueDate}");
                Console.WriteLine($"Due Date   : {i.DueDate}");
                Console.WriteLine($"Status     : {i.Status}");
            }
        }

        // ✅ ACTIVE ISSUES
        public void ListActiveIssues()
        {
            using var context = new LibraryContext();

            var issues = context.Issues
                .Where(i => i.Status == "Issued")
                .ToList();

            if (!issues.Any())
            {
                Console.WriteLine("No active issues found.");
                return;
            }

            foreach (var i in issues)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Issue Id   : {i.Id}");
                Console.WriteLine($"Student Id : {i.StudentId}");
                Console.WriteLine($"Book Id    : {i.BookId}");
                Console.WriteLine($"Due Date   : {i.DueDate}");
            }
        }

        // ✅ UPDATE DUE DATE
        public void UpdateDueDate()
        {
            using var context = new LibraryContext();

            Console.Write("Enter Issue Id: ");
            int.TryParse(Console.ReadLine(), out int id);

            var issue = context.Issues.FirstOrDefault(i => i.Id == id);

            if (issue == null)
            {
                Console.WriteLine("Issue not found.");
                return;
            }

            Console.WriteLine($"Current Due Date: {issue.DueDate}");

            Console.Write("Enter new Due Date (yyyy-MM-dd): ");
            DateTime.TryParse(Console.ReadLine(), out DateTime newDate);

            Console.Write("Admin override? (yes/no): ");
            var isAdmin = Console.ReadLine()?.ToLower();

            // 🔥 Only restrict for normal users
            if (isAdmin != "yes")
            {
                if (newDate <= issue.IssueDate)
                {
                    Console.WriteLine("Due date must be after Issue Date!");
                    return;
                }
            }

            issue.DueDate = newDate;
            context.SaveChanges();

            Console.WriteLine("Due date updated successfully!");
        }

        // ✅ CANCEL ISSUE
        public void CancelIssue()
        {
            using var context = new LibraryContext();

            Console.Write("Enter Issue Id: ");
            int.TryParse(Console.ReadLine(), out int id);

            var issue = context.Issues.FirstOrDefault(i => i.Id == id);

            if (issue == null)
            {
                Console.WriteLine("Issue not found.");
                return;
            }

            if (issue.Status == "Issued")
            {
                var book = context.Books.FirstOrDefault(b => b.Id == issue.BookId);
                if (book != null)
                    book.AvailableCopies++;
            }

            issue.Status = "Returned";

            context.SaveChanges();

            Console.WriteLine("Issue cancelled successfully!");
        }
    }
}