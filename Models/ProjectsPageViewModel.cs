using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ProjectsPageViewModel
    {
        public List<Project> Projects { get; set; } = new();
        public AddProjectsForm AddProjectForm { get; set; } = new();
        public string? SelectedStatus { get; set; }
        
    }
}
