using ExpenseTracker.Config;
using ExpenseTracker.Models;

namespace ExpenseTracker.Repository;

public class ExpenseRepository: BaseRepository<Models.Expense>
{
    
    public ExpenseRepository()
    {
        this.FileName = "expenses.json";
    }
    public  string? Id { get; set; }
    public  string? Name {get; set;}
    public  string? Description {get; set;}
    public  decimal? Amount {get; set;}
    public DateTime Date {get; set;}

    public override async Task AddAsync(Expense entity)
    {
        await this.CreateDirectoryIfNotExists();
         
     
     var res = await this.NewEntry(entity);
    }

    public override Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public override Task UpdateAsync(Expense entity)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<Expense>> GetAllAsync()
    {
        var res = await this.GetEntries<Expense>();

        foreach (var item in res)
        {
            Console.WriteLine(item);
            Console.WriteLine("-------------------");
        }
        return res;
    }

    public override async Task<Expense?> GetByIdAsync(string id)
    {
        var res = await this.GetEntryById<Expense>(id);

        Console.WriteLine(res);
        return res;
    }
}