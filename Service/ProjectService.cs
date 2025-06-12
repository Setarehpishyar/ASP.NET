using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

using Data.Entities;
using Data.Repositories;

namespace Business.Service
{
    public class ProjectService
    {
        private readonly ProjectRepo _projectRepo;

        public ProjectService(ProjectRepo projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<List<ProjectEntity>> GetAllProjectsAsync()
        {
            return await _projectRepo.GetAllAsync();
        }

        public static Project FromEntity(ProjectEntity entity)
        {
            return new Project
            {
                Title = entity.Title,
                Company = entity.Company,
                Description = entity.Description,
                ImagePath = entity.ImagePath,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status
            };
        }

        public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
        {
            return await _projectRepo.GetByIdAsync(id);
        }

        public async Task<bool> CreateProjectAsync(Models.Project model)
        {
            if (model == null) return false;

            var entity = new ProjectEntity
            {
                Title = model.Title,
                Company = model.Company,
                Description = model.Description,
                ImagePath = model.ImagePath,
                StartDate = model.StartDate,
                EndDate = model.EndDate, 
                Status = (Data.Entities.ProjectStatus)model.Status
            };

            return await _projectRepo.CreateAsync(entity);
        }

        public async Task<bool> UpdateProjectAsync(int id, Models.Project model)
        {
            var project = await _projectRepo.GetByIdAsync(id);
            if (project == null) return false;

            project.Title = model.Title;
            project.Company = model.Company;
            project.Description = model.Description;
            project.StartDate = model.StartDate;
            project.EndDate = model.EndDate;
            project.Status = (Data.Entities.ProjectStatus)model.Status;

            return await _projectRepo.UpdateAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _projectRepo.GetByIdAsync(id);
            if (project == null) return false;

            if (!string.IsNullOrEmpty(project.ImagePath) && project.ImagePath != "images/default.svg")
            {
                var fullPath = Path.Combine("wwwroot", project.ImagePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

            return await _projectRepo.DeleteAsync(id);
        }


        public async Task<List<ProjectEntity>> GetProjectsByStatusAsync(ProjectStatus status)
        {
            var all = await _projectRepo.GetAllAsync();
            return all.Where(p => p.Status == status).ToList();
        }

        public void CreateProject(ProjectEntity newProject)
        {
            _projectRepo.CreateAsync(newProject).Wait(); 
        }


        public void UpdateProject(int id, ProjectEntity updatedProject)
        {
            var existingProject = _projectRepo.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existingProject == null) return;

            existingProject.Title = updatedProject.Title;
            existingProject.Company = updatedProject.Company;
            existingProject.Description = updatedProject.Description;
            existingProject.ImagePath = updatedProject.ImagePath;
            existingProject.StartDate = updatedProject.StartDate;
            existingProject.EndDate = updatedProject.EndDate;
            existingProject.Status = updatedProject.Status;

            _projectRepo.UpdateAsync(existingProject).Wait();
        }



        public async Task CreateProjectAsync(ProjectEntity projectModel)
        {
            await _projectRepo.CreateAsync(projectModel);
        }

    }
}
