namespace MyBlog.DAL.Repositories.Interfaces;

/// <summary>
///     Реализация UnitOfWork
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task<int> CreateAsync(T item);

    Task<int> UpdateAsync(T item);

    Task<int> DeleteAsync(T item);
}