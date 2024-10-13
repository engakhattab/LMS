using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Trainee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Img { get; set; }

    public string Address { get; set; }
    public int Grade { get; set; }

    [ForeignKey("Track")] public int Track_id { get; set; }

    private Track Track { get; set; }

    public List<TraineeCourse> TraineeCourses { get; set; }
}