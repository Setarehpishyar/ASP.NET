using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Models
{
    public class AddMemberForm
    {
        public int Id { get; set; }

        [Display(Name = "Member Image", Prompt = "Select an image")]
        public IFormFile? MemberImage { get; set; }

        [Display(Name = "First Name", Prompt = "Enter first name")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name", Prompt = "Enter last name")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter email address")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "Job Title", Prompt = "Enter Job Title")]
        public string? JobTitle { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Location", Prompt = "Enter location")]
        public string? Location { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone", Prompt = "Enter phone number")]
        public string? Phone { get; set; }

        [Display(Name = "Date of Birth")]
        public int? DateOfBirth { get; set; }
        public int? DobDay { get; set; }
        public int? DobMonth { get; set; }
        public int? DobYear { get; set; }

        public AddMemberForm() { }
    }
}