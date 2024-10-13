using System.Linq.Expressions;

namespace Entities.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null);
    Task AddAsync(T entity);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
}