using System;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.UI
{
    public class FineMenu
    {
        public void Show()
        {
            var service = new ReturnService();

            while (true)
            {
                Console.WriteLine("\n--- FINE MENU ---");
                Console.WriteLine("1. Calculate Fine");
                Console.WriteLine("2. Overdue Books");
                Console.WriteLine("3. Fine Report");
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
                            Console.Write("Issue Id: ");
                            int.TryParse(Console.ReadLine(), out int id);

                            service.CalculateFine(id);
                            break;
                        }

                    case "2":
                        {
                            service.OverdueBooks();
                            break;
                        }

                    case "3":
                        {
                            service.FineReport();
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