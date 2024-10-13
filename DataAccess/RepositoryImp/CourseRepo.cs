using Entities.IRepository;
using Entities.Models;

namespace DataAccess.RepositoryImp;

public class CourseRepo : GenericRepository<Course>, Icourse
{
    private readonly DB context;

    public CourseRepo(DB context) : base(context)
    {
        this.context = context;
    }

    public void Update(Course course)
    {
        context.Update(course);
    }
}