using ExpenseTracker.Config;
namespace ExpenseTracker.Models;

public class Expense: IIdentifiable
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Example default value
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return
            $"id: {this.Id}\nname: {this.Name}\ndescription: {this.Description}\namount: {this.Amount}\ndate: {this.Date}";
    }

  
}