using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Business.Models
{
    public class AddClientForm
    {
        [Display(Name = "Client Image", Prompt = "Select a image")]
        public IFormFile? ClientImage { get; set; }

        [Display(Name = "Client Name", Prompt = "Enter client name")]
        [Required(ErrorMessage = "Required")]
        public string ClientName { get; set; } = null!;


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter email address")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]

        public string Email { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "Location", Prompt = "Enter location")]
        public string? Location { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone", Prompt = "Enter phone number")]
        public string? Phone { get; set; }

        public AddClientForm() { }


    }
}