using Entities.Models;

namespace Entities.IRepository;

public interface Iinstructor : IGenericRepository<Instructor>
{
    void Update(Instructor instructor);
}