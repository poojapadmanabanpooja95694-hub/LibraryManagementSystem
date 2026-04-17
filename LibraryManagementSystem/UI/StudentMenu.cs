using System;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.UI
{
    public class StudentMenu
    {
        public void Show()
        {
            var service = new StudentService();

            while (true)
            {
                Console.WriteLine("\n--- STUDENT MENU ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. List Students");
                Console.WriteLine("3. Edit Student");
                Console.WriteLine("4. Delete Student");
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
                            Console.Write("Name: ");
                            var name = Console.ReadLine() ?? "";

                            Console.Write("Department: ");
                            var dept = Console.ReadLine() ?? "";

                            Console.Write("Phone: ");
                            var phone = Console.ReadLine() ?? "";

                            Console.Write("Email: ");
                            var email = Console.ReadLine() ?? "";

                            service.AddStudent(name, dept, phone, email);
                            break;
                        }

                    case "2":
                        {
                            service.ListStudents();
                            break;
                        }

                    case "3":
                        {
                            Console.Write("Student Code: ");
                            var code = Console.ReadLine() ?? "";

                            Console.Write("Name: ");
                            var name = Console.ReadLine() ?? "";

                            Console.Write("Department: ");
                            var dept = Console.ReadLine() ?? "";

                            Console.Write("Phone: ");
                            var phone = Console.ReadLine() ?? "";

                            Console.Write("Email: ");
                            var email = Console.ReadLine() ?? "";

                            service.EditStudentByCode(code, name, dept, phone, email);
                            break;
                        }

                    case "4":
                        {
                            Console.Write("Student Code: ");
                            var code = Console.ReadLine() ?? "";

                            service.DeleteStudentByCode(code);
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