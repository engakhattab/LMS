using Entities.IRepository;
using Entities.Models;

namespace DataAccess.RepositoryImp;

public class InstructorRepo : GenericRepository<Instructor>, Iinstructor
{
    private readonly DB context;

    public InstructorRepo(DB context) : base(context)
    {
        this.context = context;
    }

    public void Update(Instructor instructor)
    {
        context.Update(instructor);
    }
}