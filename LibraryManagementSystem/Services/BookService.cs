using System;
using System.Linq;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class BookService
    {
        // 🔥 AUTO GENERATE BOOK CODE (B2001)
        private string GenerateBookCode(LibraryContext context)
        {
            var last = context.Books
                .OrderByDescending(b => b.BookCode)
                .FirstOrDefault();

            int num = 2000;

            if (last != null && last.BookCode.Length > 1)
            {
                num = int.Parse(last.BookCode.Substring(1));
            }

            return "B" + (num + 1);
        }

        // ✅ ADD BOOK (AUTO CODE)
        public void AddBook()
        {
            using var context = new LibraryContext();

            string code = GenerateBookCode(context);

            Console.Write("Enter Title: ");
            var title = Console.ReadLine() ?? "";

            Console.Write("Enter Author: ");
            var author = Console.ReadLine() ?? "";

            Console.Write("Enter Category: ");
            var category = Console.ReadLine() ?? "";

            Console.Write("Enter Publisher: ");
            var publisher = Console.ReadLine() ?? "";

            Console.Write("Enter Year: ");
            int.TryParse(Console.ReadLine(), out int year);

            Console.Write("Enter Total Copies: ");
            int.TryParse(Console.ReadLine(), out int totalCopies);

            if (totalCopies < 1)
            {
                Console.WriteLine("Total copies must be at least 1");
                return;
            }

            var book = new Book
            {
                BookCode = code,
                Title = title,
                Author = author,
                Category = category,
                Publisher = publisher,
                Year = year,
                TotalCopies = totalCopies,
                AvailableCopies = totalCopies,
                IsActive = true,
                CreatedOn = DateTime.Now
            };

            context.Books.Add(book);
            context.SaveChanges();

            Console.WriteLine($"Book added successfully! Code: {code}");
        }

        // ✅ LIST BOOKS
        public void ListBooks()
        {
            using var context = new LibraryContext();

            var books = context.Books
                               .Where(b => b.IsActive)
                               .ToList();

            if (!books.Any())
            {
                Console.WriteLine("No books found.");
                return;
            }

            foreach (var b in books)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Code      : {b.BookCode}");
                Console.WriteLine($"Title     : {b.Title}");
                Console.WriteLine($"Author    : {b.Author}");
                Console.WriteLine($"Category  : {b.Category}");
                Console.WriteLine($"Available : {b.AvailableCopies}/{b.TotalCopies}");
            }
        }

        // ✅ EDIT BOOK
        public void EditBook()
        {
            using var context = new LibraryContext();

            Console.Write("Enter Book Code to Edit: ");
            var code = Console.ReadLine();

            var book = context.Books
                              .FirstOrDefault(b => b.BookCode == code && b.IsActive);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.Write("Enter New Title: ");
            book.Title = Console.ReadLine() ?? "";

            Console.Write("Enter New Author: ");
            book.Author = Console.ReadLine() ?? "";

            Console.Write("Enter New Category: ");
            book.Category = Console.ReadLine() ?? "";

            Console.Write("Enter New Publisher: ");
            book.Publisher = Console.ReadLine() ?? "";

            Console.Write("Enter New Total Copies: ");
            int.TryParse(Console.ReadLine(), out int newTotal);

            int issuedBooks = book.TotalCopies - book.AvailableCopies;

            if (newTotal < issuedBooks)
            {
                Console.WriteLine("Cannot reduce below issued count.");
                return;
            }

            book.TotalCopies = newTotal;
            book.AvailableCopies = newTotal - issuedBooks;

            context.SaveChanges();

            Console.WriteLine("Book updated successfully!");
        }

        // ✅ DELETE BOOK
        public void DeleteBook()
        {
            using var context = new LibraryContext();

            Console.Write("Enter Book Code to Delete: ");
            var code = Console.ReadLine();

            var book = context.Books
                .FirstOrDefault(b => b.BookCode == code && b.IsActive);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (book.AvailableCopies < book.TotalCopies)
            {
                Console.WriteLine("Cannot delete. Book is issued.");
                return;
            }

            book.IsActive = false;

            context.SaveChanges();

            Console.WriteLine("Book deleted successfully!");
        }
    }
}