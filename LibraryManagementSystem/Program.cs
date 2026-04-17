using LibraryManagementSystem.Services;
using LibraryManagementSystem.UI;

while (true)
{
    Console.WriteLine("\n---- MAIN MENU ----");
    Console.WriteLine("1. Student Registration");
    Console.WriteLine("2. Book Registration");
    Console.WriteLine("3. Book Issue Details");
    Console.WriteLine("4. Book Return Details");
    Console.WriteLine("5. Fine Management");
    Console.WriteLine("6. Exit");

    Console.Write("Enter choice: ");
    int.TryParse(Console.ReadLine(), out int choice);

    switch (choice)
    {
        case 1:
            new StudentMenu().Show();
            break;

        case 2:
            new BookMenu().Show();
            break;

        case 3:
            new IssueMenu().Show();
            break;

        case 4:
            new ReturnMenu().Show();
            break;

        case 5:
            new FineMenu().Show();
            break;

        case 6:
            return;
    }
}
