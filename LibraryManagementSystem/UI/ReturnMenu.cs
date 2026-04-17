using System;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.UI
{
    public class ReturnMenu
    {
        public void Show()
        {
            var service = new ReturnService();

            while (true)
            {
                Console.WriteLine("\n--- RETURN MENU ---");
                Console.WriteLine("1. Add Return");
                Console.WriteLine("2. List Returns");
                Console.WriteLine("3. Edit Return");
                Console.WriteLine("4. Delete Return");
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

                            service.AddReturn(id);
                            break;
                        }

                    case "2":
                        {
                            service.ListReturns();
                            break;
                        }

                    case "3":
                        {
                            Console.Write("Return Id: ");
                            var rid = Console.ReadLine() ?? "";

                            Console.Write("New Date (yyyy-MM-dd): ");
                            DateTime.TryParse(Console.ReadLine(), out DateTime nd);

                            service.EditReturn(rid, nd);
                            break;
                        }

                    case "4":
                        {
                            Console.Write("Return Id: ");
                            var del = Console.ReadLine() ?? "";

                            service.DeleteReturn(del);
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