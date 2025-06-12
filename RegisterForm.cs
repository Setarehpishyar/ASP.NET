using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace WebApp.Models
{
    public class RegisterForm
    {


        [Display(Name = "Full Name", Prompt = "Enter full name")]
        [Required(ErrorMessage = "Required")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Email", Prompt = "Enter email address")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "Password must be confirmed")]
        [Required(ErrorMessage = "This field is required")]
        public string ConfirmPassword { get; set; } = null!;



        [Display(Name = "Terms And Conditions", Prompt = "I accept the terms and conditions.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = " You must accept the terms and conditions to use this site.")]
        public bool TermsAndConditions { get; set; }
        

    }
}