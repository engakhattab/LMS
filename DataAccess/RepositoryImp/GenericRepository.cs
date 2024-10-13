using System.Linq.Expressions;
using Entities.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.RepositoryImp;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DB context;
    protected DbSet<T> dbSet;

    public GenericRepository(DB context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
        string? IncludeWord = null)
    {
        IQueryable<T> query = dbSet;
        if (predicate != null) query = query.Where(predicate);
        if (IncludeWord != null)
            foreach (var item in IncludeWord.Split
                         (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(item);
        return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, string? IncludeWord = null)
    {
        IQueryable<T> query = dbSet;
        if (predicate != null) query = query.Where(predicate);
        if (IncludeWord != null)
            foreach (var item in IncludeWord.Split
                         (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(item);
        return (await query.FirstOrDefaultAsync())!;
    }

    public Task RemoveAsync(T entity)
    {
        dbSet.Remove(entity);
        return Task.CompletedTask;
        // we need to add a logger
    }

    public Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }
}