using ExpenseTracker.Config;

namespace ExpenseTracker.Repository;

public abstract class BaseRepository<T>: Store where T : IIdentifiable
{
    // Abstract methods to be implemented by derived classes
    public abstract Task AddAsync(T entity);
    public abstract Task<T?> GetByIdAsync(string id);
    public abstract Task<IEnumerable<T>> GetAllAsync();
    public abstract Task UpdateAsync(T entity);
    public abstract Task DeleteAsync(string id);

    // Optionally, include common logic shared across repositories
    protected async Task EnsureDirectoryExistsAsync(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    protected async Task WriteToFileAsync(string path, string content)
    {
        await File.WriteAllTextAsync(path, content);
    }

    protected async Task<string> ReadFromFileAsync(string path)
    {
        return await File.ReadAllTextAsync(path);
    }
}