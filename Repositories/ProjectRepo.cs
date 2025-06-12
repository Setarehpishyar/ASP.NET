using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProjectRepo
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepo(ApplicationDbContext context)
        {
            _context = context;
        }

   
        public async Task<List<ProjectEntity>> GetAllAsync()
        {
            return await _context.Projects
                .AsNoTracking() 
                .ToListAsync();
        }

       
        public async Task<ProjectEntity?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        
        public async Task<bool> CreateAsync(ProjectEntity project)
        {
            try
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
               
                return false;
            }
        }

        
        public async Task<bool> UpdateAsync(ProjectEntity updatedProject)
        {
            var existing = await _context.Projects.FirstOrDefaultAsync(p => p.Id == updatedProject.Id);
            if (existing == null) return false;

            existing.Title = updatedProject.Title;
            existing.Company = updatedProject.Company;
            existing.Description = updatedProject.Description;
            existing.StartDate = updatedProject.StartDate;
            existing.EndDate = updatedProject.EndDate;
            existing.Status = updatedProject.Status;
            existing.ImagePath = updatedProject.ImagePath;

            try
            {
                _context.Projects.Update(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            try
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
