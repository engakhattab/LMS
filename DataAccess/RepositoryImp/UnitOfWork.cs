using Entities.IRepository;
using Entities.Models;

namespace DataAccess.RepositoryImp;

public class UnitOfWork : IunitOfWork
{
    // DB context;
    private readonly DB context;

    public UnitOfWork(DB context)
    {
        // context = new DB();
        this.context = context;
        instructorrepo = new InstructorRepo(context);
        Courserepo = new CourseRepo(context);
    }

    public Iinstructor instructorrepo { get; }

    public Icourse Courserepo { get; }

    public async Task<int> Complete()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}