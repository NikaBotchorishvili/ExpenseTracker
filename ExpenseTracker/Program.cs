using Microsoft.Extensions.DependencyInjection;
using ExpenseTracker.Repository;
using ExpenseTracker.Models;

var services = new ServiceCollection();
services.AddSingleton<ExpenseRepository>();
var serviceProvider = services.BuildServiceProvider();

var expenseRepository = serviceProvider.GetService<ExpenseRepository>();
var isRunning = true;


if (expenseRepository == null)
{
    Console.WriteLine("Expense Service isn't available.");
    return;
}


Console.WriteLine("Welcome to Expense Tracker app");
Console.WriteLine("Following commands are valid: add, exit, edit, view, remove, help.");
while (isRunning)
{
    var cmd = Console.ReadLine();
    switch (cmd)
    {
        case "add":
            Console.WriteLine("Enter expense name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Enter expense description");
            var description = Console.ReadLine();
            var date = DateTime.Now;
    
            Console.WriteLine("Enter the expense price.");
            if (decimal.TryParse(Console.ReadLine(), out var amount))
            {
                var expense = new Expense
                {
                    Name = name,
                    Description = description,
                    Amount = amount,
                    Date = date
                };
                await expenseRepository.AddAsync(expense);
            }
            else
            {
                Console.WriteLine("Invalid amount");
            }
            break;
        case "view":
            await expenseRepository.GetAllAsync();
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