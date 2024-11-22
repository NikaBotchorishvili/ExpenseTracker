using ExpenseTracker.Models;

var isRunning = true;

Console.WriteLine("Welcome to Expense Tracker app");
Console.WriteLine("Following commands are valid: add, exit, edit, view, remove, help.");
var expense = new Expense();
while (isRunning)
{
    var cmd = Console.ReadLine();
    switch (cmd)
    {
        case "add":
            await expense.AddExpense();
            break;
        case "view":
            await expense.GetExpenses();
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