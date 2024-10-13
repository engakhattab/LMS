using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Instructor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Img { get; set; }
    public decimal Salary { get; set; }
    public string Adress { get; set; }

    [ForeignKey("Track")] public int Track_ID { get; set; }

    public Track Track { get; set; }
    private List<Course> Courses { get; set; }
}