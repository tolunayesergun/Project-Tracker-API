using ProjectTracker_API.Models.Concretes;

namespace ProjectTracker_API.Models.Entities
{
    public class Entry : SharedProperties
    {
        public string EntryName { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
