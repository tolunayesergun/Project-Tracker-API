using ProjectTracker_API.Models.Concretes;

namespace ProjectTracker_API.Models.Entities
{
    public class Project : SharedProperties
    {
        public string ProjectName { get; set; }
        public string ProjectDesc { get; set; }
        public string ProjectCode { get; set; }
    }
}
