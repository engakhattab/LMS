using Entities.IRepository;
using Entities.Models;
using Entities.ViewModels;
using LMS.Common.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.Controllers;

public class AccountController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IunitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(IunitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        LoginVM loginVM = new()
        {
            RedirectUrl = returnUrl
        };

        return View(loginVM);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager
                .PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                    return RedirectToAction("Index", "Home");
                return LocalRedirect(loginVM.RedirectUrl);
            }

            ModelState.AddModelError("", "Invalid login attempt.");
        }

        return View(loginVM);
    }

    public IActionResult Register()
    {
        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).Wait();
        }

        RegisterVM registerVM = new()
        {
            RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            })
        };

        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        ApplicationUser user = new()
        {
            Name = registerVM.Name,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
            NormalizedEmail = registerVM.Email.ToUpper(),
            EmailConfirmed = true,
            UserName = registerVM.Email
        };
        var result = await _userManager.CreateAsync(user, registerVM.Password);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(registerVM.Role))
                await _userManager.AddToRoleAsync(user, registerVM.Role);
            else
                await _userManager.AddToRoleAsync(user, SD.Role_User);

            await _signInManager.SignInAsync(user, false);
            if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                return RedirectToAction("Index", "Home");
            return LocalRedirect(registerVM.RedirectUrl);
        }

        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);

        registerVM.RoleList = _roleManager.Roles.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Name
        });
        return View(registerVM);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}