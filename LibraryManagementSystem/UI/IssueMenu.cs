using System;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.UI
{
    public class IssueMenu
    {
        public void Show()
        {
            var service = new IssueService();

            while (true)
            {
                Console.WriteLine("\n--- ISSUE MENU ---");
                Console.WriteLine("1. Issue Book");
                Console.WriteLine("2. List All Issues");
                Console.WriteLine("3. Active Issues");
                Console.WriteLine("4. Update Due Date");
                Console.WriteLine("5. Cancel Issue");
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
                            Console.Write("Student Code: ");
                            var s = Console.ReadLine() ?? "";

                            Console.Write("Book Code: ");
                            var b = Console.ReadLine() ?? "";

                            service.IssueBookByCode(s, b);
                            break;
                        }

                    case "2":
                        {
                            service.ListAllIssues();
                            break;
                        }

                    case "3":
                        {
                            service.ListActiveIssues();
                            break;
                        }

                    case "4":
                        {
                            service.UpdateDueDate();
                            break;
                        }

                    case "5":
                        {
                            service.CancelIssue();
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