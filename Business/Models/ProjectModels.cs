
namespace Business.Models
{
    public enum ProjectStatus
    {
        InProgress,
        Completed,
        OnHold,
        Canceled
    }

    public class Project
    {
        public string Title { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(4 * 7);

        public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;

        public int WeeksLeft => (Deadline - DateTime.Today).Days / 7;
    }
}