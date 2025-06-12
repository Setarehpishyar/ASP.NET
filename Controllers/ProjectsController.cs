using Microsoft.AspNetCore.Mvc;
using Business.Models;
using Business.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Entities;
using System.Linq;

using Microsoft.AspNetCore.Authorization;



namespace WebApp.Controllers

{
    [Route("projects")]
    public class ProjectsController : Controller
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }
        

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Projects(string? status)
        {
            var projectEntities = await _projectService.GetAllProjectsAsync();

            var allProjects = projectEntities.Select(p => new Project
            {
                Id = p.Id,
                Title = p.Title,
                Company = p.Company,
                Description = p.Description,
                ImagePath = p.ImagePath,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
            }).ToList();

            var projects = allProjects;
            if (!string.IsNullOrEmpty(status) && Enum.TryParse(status, out ProjectStatus parsedStatus))
            {
                projects = allProjects.Where(p => p.Status == parsedStatus).ToList();
            }

          

            var viewModel = new ProjectsPageViewModel
            {
                Projects = projects,
                AddProjectForm = new AddProjectsForm(),
                SelectedStatus = status
            };

            return View(viewModel);
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(AddProjectsForm form)
        {
            if (!ModelState.IsValid)
            {
                var projectEntities = await _projectService.GetAllProjectsAsync();
                var projects = projectEntities.Select(p => new Project
                {
                    Title = p.Title,
                    Company = p.Company,
                    Description = p.Description,
                    ImagePath = p.ImagePath,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status
                }).ToList();

                var viewModel = new ProjectsPageViewModel
                {
                    Projects = projects,
                    AddProjectForm = form
                };

                return View("Projects", viewModel);
            }

            var newProject = new ProjectEntity
            {
                Title = form.ProjectName,
                Company = form.ClientName,
                Description = form.Description ?? "",
                ImagePath = form.ProjectImage?.FileName ?? "images/default.svg",
                StartDate = form.StartDate,
                EndDate = form.EndDate,
                Status = form.Status
            };

            _projectService.CreateProject(newProject);
            return RedirectToAction("Projects");
        }

        
        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            var editForm = new EditProjectsForm
            {
                ProjectName = project.Title,
                ClientName = project.Company,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status
            };

            return View("EditProject", editForm);
        }


        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, EditProjectsForm form)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProject", form);
            }

            string imagePath = "images/default.svg";

            if (form.ProjectImage != null && form.ProjectImage.Length > 0)
            {
                var fileExtension = Path.GetExtension(form.ProjectImage.FileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}"; 
                var saveFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var savePath = Path.Combine(saveFolder, uniqueFileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await form.ProjectImage.CopyToAsync(stream);
                }

                imagePath = $"images/{uniqueFileName}";
            }


            var updatedProject = new ProjectEntity
            {
                Id = id,
                Title = form.ProjectName,
                Company = form.ClientName,
                Description = form.Description ?? "",
                StartDate = form.StartDate,
                EndDate = form.EndDate,
                Status = form.Status,
                ImagePath = imagePath 
            };

            _projectService.UpdateProject(id, updatedProject);
            return RedirectToAction("Projects");
        }

       
        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return RedirectToAction("Projects");
        }

        
        [HttpGet]
        [Route("getproject")]

        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            return Json(new
            {
                id = project.Id,
                title = project.Title,
                company = project.Company,
                description = project.Description,
                startDate = project.StartDate.ToString("yyyy-MM-dd"),
                endDate = project.EndDate.ToString("yyyy-MM-dd"),
                budget = project.Budget,
                members = project.Members,
                status = project.Status.ToString()
            });
        }

    }
}
