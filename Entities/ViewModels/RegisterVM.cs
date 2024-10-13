using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entities.ViewModels;

public class RegisterVM
{
    [Required] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; }

    [Required] public string Name { get; set; }

    [Display(Name = "Phone Number")] public string? PhoneNumber { get; set; }


    public string? RedirectUrl { get; set; }
    public string? Role { get; set; }

    [ValidateNever] public IEnumerable<SelectListItem>? RoleList { get; set; }
}