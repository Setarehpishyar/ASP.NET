using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Business.Models
{
    public class EditProjectsForm
    {
        [Display(Name = "Project Image", Prompt = "Select an image")]
        public IFormFile? ProjectImage { get; set; }


        [Display(Name = "Project Name", Prompt = "Enter project name")]
        [Required(ErrorMessage = "Required")]
        public string ProjectName { get; set; } = null!;

        [Display(Name = "Client Name", Prompt = "Enter Client name")]
        [Required(ErrorMessage = "Required")]
        public string ClientName { get; set; } = null!;


        [DataType(DataType.Text)]
        [Display(Name = "Description", Prompt = "Type something")]
        public string? Description { get; set; }


        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;


        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(30);

        [DataType(DataType.Text)]
        [Display(Name = "Members", Prompt = "Search members")]

        public string? Members { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Budget", Prompt = "$0")]
        [Required(ErrorMessage = "Required")]
        public string? Budget { get; set; }

        public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;
    }
}