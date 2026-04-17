using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using System;
using System.Linq;

namespace LibraryManagementSystem.Services
{
    public class ReturnService
    {
        // ✅ ADD RETURN
        public void AddReturn(int issueId)
        {
            using var context = new LibraryContext();

            var issue = context.Issues.FirstOrDefault(i => i.Id == issueId);

            if (issue == null)
            {
                Console.WriteLine("Invalid IssueId!");
                return;
            }

            if (issue.Status == "Returned")
            {
                Console.WriteLine("Book already returned!");
                return;
            }

            // 🔥 Simulation Mode
            Console.Write("Simulate return date (yyyy-MM-dd or Enter): ");
            var input = Console.ReadLine();

            DateTime returnDate;

            if (!string.IsNullOrWhiteSpace(input))
                DateTime.TryParse(input, out returnDate);
            else
                returnDate = DateTime.Now;

            // ❗ RULE VALIDATION
            if (returnDate < issue.IssueDate)
            {
                Console.WriteLine("Return date cannot be before Issue Date!");
                return;
            }

            // 💰 Fine Calculation
            decimal fine = 0;
            int finePerDay = 5;

            if (returnDate > issue.DueDate)
            {
                int lateDays = (returnDate - issue.DueDate).Days;
                fine = lateDays * finePerDay;
            }

            var returnObj = new Return
            {
                ReturnId = "R" + DateTime.Now.Ticks,
                IssueId = issue.Id,
                ReturnDate = returnDate,
                FineAmount = fine,
                Remarks = "Returned"
            };

            context.Returns.Add(returnObj);

            issue.Status = "Returned";

            var book = context.Books.FirstOrDefault(b => b.Id == issue.BookId);
            if (book != null)
                book.AvailableCopies++;

            context.SaveChanges();

            Console.WriteLine($"Return Success | Fine: {fine}");
        }

        // ✅ LIST RETURNS
        public void ListReturns()
        {
            using var context = new LibraryContext();

            var list = context.Returns.ToList();

            if (!list.Any())
            {
                Console.WriteLine("No returns found.");
                return;
            }

            foreach (var r in list)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"ReturnId : {r.ReturnId}");
                Console.WriteLine($"IssueId  : {r.IssueId}");
                Console.WriteLine($"Date     : {r.ReturnDate}");
                Console.WriteLine($"Fine     : {r.FineAmount}");
            }
        }

        // ✅ EDIT RETURN
        public void EditReturn(string returnId, DateTime newDate)
        {
            using var context = new LibraryContext();

            var ret = context.Returns.FirstOrDefault(r => r.ReturnId == returnId);

            if (ret == null)
            {
                Console.WriteLine("Return not found!");
                return;
            }

            var issue = context.Issues.First(i => i.Id == ret.IssueId);

            if (newDate < issue.IssueDate)
            {
                Console.WriteLine("Invalid date!");
                return;
            }

            decimal fine = 0;
            int finePerDay = 5;

            if (newDate > issue.DueDate)
            {
                int lateDays = (newDate - issue.DueDate).Days;
                fine = lateDays * finePerDay;
            }

            ret.ReturnDate = newDate;
            ret.FineAmount = fine;

            context.SaveChanges();

            Console.WriteLine($"Updated! New Fine: {fine}");
        }

        // ✅ DELETE RETURN
        public void DeleteReturn(string returnId)
        {
            using var context = new LibraryContext();

            var ret = context.Returns.FirstOrDefault(r => r.ReturnId == returnId);

            if (ret == null)
            {
                Console.WriteLine("Return not found!");
                return;
            }

            var issue = context.Issues.First(i => i.Id == ret.IssueId);
            var book = context.Books.First(b => b.Id == issue.BookId);

            issue.Status = "Issued";
            book.AvailableCopies--;

            context.Returns.Remove(ret);
            context.SaveChanges();

            Console.WriteLine("Deleted successfully!");
        }

        // ✅ CALCULATE FINE
        public void CalculateFine(int issueId)
        {
            using var context = new LibraryContext();

            var issue = context.Issues.FirstOrDefault(i => i.Id == issueId);

            if (issue == null)
            {
                Console.WriteLine("Invalid IssueId");
                return;
            }

            DateTime today = DateTime.Now;

            decimal fine = 0;
            int finePerDay = 5;

            if (today > issue.DueDate)
            {
                int lateDays = (today - issue.DueDate).Days;
                fine = lateDays * finePerDay;
            }

            Console.WriteLine($"Fine = {fine}");
        }

        // ✅ OVERDUE
        public void OverdueBooks()
        {
            using var context = new LibraryContext();

            var list = context.Issues
                .Where(i => i.Status == "Issued" && i.DueDate < DateTime.Now)
                .ToList();

            foreach (var i in list)
            {
                Console.WriteLine($"IssueId: {i.Id} | Due: {i.DueDate}");
            }
        }

        // ✅ FINE REPORT
        public void FineReport()
        {
            using var context = new LibraryContext();

            var list = context.Returns.Where(r => r.FineAmount > 0).ToList();

            foreach (var r in list)
            {
                Console.WriteLine($"ReturnId: {r.ReturnId} | Fine: {r.FineAmount}");
            }
        }
    }
}