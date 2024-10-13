using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Degree { get; set; }
    public int Mindegree { get; set; }
    public decimal price { get; set; }
    public string Description { get; set; }
    public string img { get; set; }
    public int seats { get; set; }


    [ForeignKey("Track")] public int Track_ID { get; set; }

    public Track Track { get; set; }


    [ForeignKey("Instructor")] public int Instructor_ID { get; set; }

    public Instructor Instructor { get; set; }


    public List<TraineeCourse> TraineeCourses { get; set; }
}