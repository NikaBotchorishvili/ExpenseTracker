using System.Text.Json;
 
namespace ExpenseTracker.Config;

public class Store
{
    protected string FileName { get; set; } = String.Empty;

    protected async Task<List<T>> GetEntries<T>() where T: IIdentifiable
    {
        string json = await File.ReadAllTextAsync(FileName);

        var items = JsonSerializer.Deserialize<List<T>>(json)?? new List<T>();
        
        return items;
    }
    
    
    protected async Task CreateDirectoryIfNotExists()
    {
        try
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, FileName);
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
            Console.WriteLine("Hello From NewEntry");
            item.Id = Guid.NewGuid().ToString();
      
            string json = await File.ReadAllTextAsync(this.FileName);
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
            await File.WriteAllTextAsync(FileName, updatedJson);
            return updatedJson;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error While adding an entry:\n{ex.Message}");
            throw;
        }
    }

    protected async Task<T> GetEntryById<T>(string id) where T: IIdentifiable
    {
        try
        {
            string json = await File.ReadAllTextAsync(FileName);

            var items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            Console.WriteLine("\n\n\n" + id + "\n\n\n");
            var entry = items.FirstOrDefault((item) => item.Id == id);
            if (entry == null)
            {
                throw new Exception($"Entry with id {id} not found.");
            }

            return entry;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while getting an entry:\n{ex.Message}");
            throw;
        }
    }
}