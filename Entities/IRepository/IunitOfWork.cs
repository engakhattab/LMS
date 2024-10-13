namespace Entities.IRepository;

public interface IunitOfWork : IDisposable, IAsyncDisposable
{
    Iinstructor instructorrepo { get; }
    Icourse Courserepo { get; }
    Task<int> Complete();
}