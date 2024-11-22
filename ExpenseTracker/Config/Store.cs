using System.Text.Json;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Config;

public class Store
{
    private string _fileName = "data.json";

    protected async Task<List<T>> Get<T>() where T: IIdentifiable
    {
        string json = await File.ReadAllTextAsync(_fileName);

        var items = JsonSerializer.Deserialize<List<T>>(json)?? new List<T>();

        foreach (var item in items)
        {
            Console.WriteLine(item);
            Console.WriteLine("-------------------");
        }
        return items;
    }
    
    
    protected async Task CreateDirectoryIfNotExists()
    {
        try
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, _fileName);
            string? directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found. Initializing {filePath}.");
                await File.WriteAllTextAsync(filePath, "[]"); // Initialize with an empty JSON array
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating file: {ex.Message}");
        }
    }

    protected async Task<string> NewEntry<T>(T item) where T: IIdentifiable
    {
        try
        {

            item.Id = Guid.NewGuid().ToString();
            var jsonString = JsonSerializer.Serialize(item);
            string json = await File.ReadAllTextAsync(this._fileName);
            List<T> items;
            if (!string.IsNullOrWhiteSpace(json))
            {
                items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            else
            {
                items = new List<T>();
            }

            items.Add(item);

            string updatedJson = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true});
            await File.WriteAllTextAsync(_fileName, updatedJson);
            return updatedJson;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error While adding an entry:\n{ex.Message}");
            throw;
        }
    }
}