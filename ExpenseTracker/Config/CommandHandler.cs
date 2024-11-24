using System.Text.Json;

namespace ExpenseTracker.Config;

using Repository;

public static class CommandHandler
{
    public static async Task HandleCommandAsync<T>(
        string command,
        Guid id,
        BaseRepository<T> repository,
        string FileName,
        List<string> columns = null) where T : class, IIdentifiable, new()    {
        T? entity;
        switch (command)
        {
            case "get":
                entity = await repository.GetByIdAsync(id.ToString());
                Console.WriteLine(entity != null ? $"Entity found: {entity}" : "Entity not found.");
                break;

            case "delete":
                await repository.DeleteAsync(id.ToString());
                Console.WriteLine($"Entity with ID {id} deleted.");
                break;

            case "update":
                entity = await repository.GetByIdAsync(id.ToString());
                if (entity == null)
                {
                    Console.WriteLine("Entity not found.");
                    return;
                }

                if (columns.Count == 0)
                {
                    Console.WriteLine("No columns specified for update.");
                    return;
                }

                foreach (var column in columns)
                {
                    var propertyInfo = typeof(T).GetProperty(column);
                    if (propertyInfo == null || !propertyInfo.CanWrite)
                    {
                        Console.WriteLine($"Property '{column}' does not exist or is not writable.");
                        continue;
                    }

                    Console.WriteLine($"Enter value for '{column}':");
                    var inputValue = Console.ReadLine();
                    try
                    {
                        var targetType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ??
                                         propertyInfo.PropertyType;
                        var convertedValue = inputValue == null
                            ? null
                            : Convert.ChangeType(inputValue, targetType);
                        propertyInfo.SetValue(entity, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to update property '{column}': {ex.Message}");
                    }
                }

                var items = await File.ReadAllTextAsync(FileName);
                if (!string.IsNullOrEmpty(items))
                {
                    var parsedJson = JsonSerializer.Deserialize<List<T>>(items);
                    Console.WriteLine(parsedJson);
                }
                
                await repository.UpdateAsync(entity);
                Console.WriteLine($"Entity with ID {id} updated.");
                break;

            default:
                Console.WriteLine($"Unknown command: {command}. Supported commands are: get, delete, update.");
                break;
        }
    }
}