using Entities.IRepository;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers;

public class InstructorController : Controller
{
    private readonly IunitOfWork _unitOfWork;

    public InstructorController(IunitOfWork unitofwork)
    {
        _unitOfWork = unitofwork;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _unitOfWork.instructorrepo.GetAllAsync(null, "Track");
        return View("Instrucotr", result as List<Instructor>);
    }
}