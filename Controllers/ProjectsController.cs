using Microsoft.AspNetCore.Mvc;

using Business.Models;

namespace Business.Controllers
{
    [Route("projects")]
    public class ProjectsController : Controller
    {
       
        private static List<Project> _projects = new List<Project>
        {
            new Project
            {
                Title = "Website Redesign",
                Company = "GitLab Inc.",
                Description = "It is necessary to develop a website redesign in a corporate style.",
                Deadline = new DateTime(2025, 6, 15),
                Status  = ProjectStatus.InProgress
            },
            new Project
            {
                Title = "Landing Page",
                Company = "Bitbucket, Inc.",
                Description = "It is necessary to create a landing together with the development of design.",
                Deadline = new DateTime(2025, 5, 10),
                Status = ProjectStatus.InProgress
            },
            
        };

        [HttpGet]
        [Route("")]
        public IActionResult Projects()
        {
            return View("Projects");
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(AddProjectsForm form)
        {
            if (!ModelState.IsValid)
            {
                return View("Projects");
            }

            var newProject = new Project
            {
                Title = form.ProjectName,
                Company = form.ClientName,
                Description = form.Description ?? "",
                ImagePath = form.ProjectImage?.FileName ?? "images/default.svg",
                Deadline = form.EndDate,  
                Status = form.Status,
                StartDate = form.StartDate 
            };


            _projects.Add(newProject);
            return RedirectToAction("Projects");
        }
    }
}

