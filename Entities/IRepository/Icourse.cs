using Entities.Models;

namespace Entities.IRepository;

public interface Icourse : IGenericRepository<Course>
{
    void Update(Course course);
}