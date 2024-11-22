using ExpenseTracker.Models;

var isRunning = true;

Console.WriteLine("Welcome to Expense Tracker app");
Console.WriteLine("Following commands are valid: add, exit, edit, remove, help.");
while (isRunning)

{
    var cmd = Console.ReadLine();
    switch (cmd)
    {
        case "add":
            break;
        case "edit":
            break;
        case "remove":
            break;
        case "exit":
            isRunning = false;
            break;
        case "help":
            Console.WriteLine("Commands: add, exit, edit, remove, help.");
            break;
        default:
            Console.WriteLine("Invalid command. Valid commands are: add, edit, remove, help and exit.");
            break;
    }
}