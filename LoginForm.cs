using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class LoginForm
    {
        [Display(Name = "Email", Prompt = "Enter email address")]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
