using System;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.UI
{
    public class BookMenu
    {
        public void Show()
        {
            var service = new BookService();

            while (true)
            {
                Console.WriteLine("\n--- BOOK MENU ---");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. List Books");
                Console.WriteLine("3. Edit Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("--------------------");
                Console.WriteLine("B = Back");
                Console.WriteLine("M = Main Menu");
                Console.WriteLine("0 = Exit");

                Console.Write("Enter choice: ");
                var choice = Console.ReadLine()?.ToUpper();

                switch (choice)
                {
                    case "1":
                        {
                            service.AddBook();
                            break;
                        }

                    case "2":
                        {
                            service.ListBooks();
                            break;
                        }

                    case "3":
                        {
                            service.EditBook();
                            break;
                        }

                    case "4":
                        {
                            service.DeleteBook();
                            break;
                        }

                    case "B":
                        return;

                    case "M":
                        return;

                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }
}