
using System;

using System;

namespace Data.Entities
{
    public class ProjectEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; }
        public decimal Budget { get; set; }
        public string ? Members { get; set; }
    }
}