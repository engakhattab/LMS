namespace Entities.Models;

public class Track
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string img { get; set; }

    public List<Instructor> Instructors { get; set; }
    public List<Trainee> Trainees { get; set; }
}