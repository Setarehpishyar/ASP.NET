using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Business.Models
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
        [Display(Name = "Confirm Password", Prompt = "Enter Password")]
        [Required(ErrorMessage = "Password is required")]
        public string ConfirmPassword { get; set; } = null!;

        public RegisterForm() { }
    }
}