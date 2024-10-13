using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models;

public class DB : IdentityDbContext<ApplicationUser>
{
    public DB(DbContextOptions<DB> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Trainee> Trainees { get; set; }
    public DbSet<CrsResults> Results { get; set; }
    public DbSet<TraineeCourse> TraineeCourses { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    // public DB()
    // {
    //     // throw new NotImplementedException();
    // }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(
    //         "Data Source=localhost;Initial Catalog=LMS; User ID=sa;Password=Mm181120011#;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    //     base.OnConfiguring(optionsBuilder);
    // }
}