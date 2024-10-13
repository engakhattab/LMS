using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class CrsResults
{
    public int Id { get; set; }
    public int Degree { get; set; }
    public int Crs_ID { get; set; }

    [ForeignKey("Trainee")] public int Trainee_ID { get; set; }
}