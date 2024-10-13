using System.Security.Claims;
using Entities.IRepository;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers;

public class CourseController : Controller
{
    private readonly DB _db;
    private readonly IunitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public CourseController(IunitOfWork unitofwork, DB db, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitofwork;
        _db = db;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _unitOfWork.Courserepo.GetAllAsync(null, "Instructor");

        return View("Course", result as List<Course>);
    }

    public async Task<IActionResult> ShowCourse(int id)
    {
        var crs = await _unitOfWork.Courserepo.GetFirstOrDefaultAsync(x => x.Id == id, "Instructor");
        return View("CourseDetails", crs);
    }

    [HttpPost]
    public async Task<IActionResult> Enroll(int courseId)
    {
        // var username = User.FindFirstValue(ClaimTypes.Name);

        // get the name of the logged in user <ApplicationUser>
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        var username = user.Name;
        var trainee = await _db.Trainees.FirstOrDefaultAsync(x => x.Name == username);
        if (trainee == null) return NotFound("Trainee not found");

        var traineeCourse = new TraineeCourse
        {
            CourseId = courseId,
            TraineeId = trainee.Id
        };
        _db.TraineeCourses.Add(traineeCourse);
        await _db.SaveChangesAsync();
        return RedirectToAction("ShowCourse", new { id = courseId });
    }

    public async Task<IActionResult> MyCourses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        var username = user.Name;
        var trainee = await _db.Trainees.FirstOrDefaultAsync(x => x.Name == username);
        if (trainee == null) return NotFound("Trainee not found");

        var courses = await _db.TraineeCourses
            .Where(tc => tc.TraineeId == trainee.Id)
            .Include(tc => tc.Course)
            .Select(tc => tc.Course)
            .ToListAsync();
        return View("MyCourses", courses);
    }

    public async Task<IActionResult> InstructorCourses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId);
        var username = user.Name;
        var instructor = await _db.Instructors.FirstOrDefaultAsync(x => x.Name == username);
        if (instructor == null) return NotFound("Instructor not found");

        var courses = await _db.Courses
            .Where(c => c.Instructor_ID == instructor.Id)
            .ToListAsync();


        return View("InstructorCourses", courses);
    }
}