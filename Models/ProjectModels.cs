using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;
      
        public string IconPath => ImagePath;
     
        public List<string> AvatarPaths { get; set; } = new List<string>();

        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(30); 
        public DateTime Deadline => EndDate;

        public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;

        public int WeeksLeft => (Deadline - DateTime.Today).Days / 7;


    }
}

