using System.Text.Json;
using System.Text.Json;
namespace ExpenseTracker.Models;

public class Expense: Config.Store, Config.IIdentifiable
{
    public string Id { get; set; }
    public string? Name {get; set;}
    public string? Description {get; set;}
    public decimal? Amount {get; set;}
    public DateTime Date {get; set;}

    public override string ToString()
    {
        return $"id:{this.Id}\nname: {this.Name}\ndescription: {this.Description}\namount: {this.Amount}\ndate: {this.Date}";
    }

    public async Task AddExpense()
    {
        await this.CreateDirectoryIfNotExists();
        
        Console.WriteLine("Enter expense name: ");
        this.Name = Console.ReadLine();
        Console.WriteLine("Enter expense description");
        this.Description = Console.ReadLine();
        Console.WriteLine("Enter the expense price.");
        string? tempAmount = Console.ReadLine();
        this.Date = DateTime.Now;

        if (!string.IsNullOrEmpty(tempAmount))
        {
            this.Amount = decimal.Parse(tempAmount);
        }
        var newExpense = new Expense
        {
            Name = this.Name,
            Description = this.Description,
            Amount = this.Amount,
            Date = this.Date
        };
        await this.NewEntry(newExpense);
    }

    public async Task<string> GetExpenses()
    {
        await this.Get<Expense>();
        
        return "";
    }
}