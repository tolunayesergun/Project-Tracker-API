using ProjectTracker_API.Models.Concretes;

namespace ProjectTracker_API.Models.Entities
{
    public class EntryUser: SharedProperties
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int EntryId { get; set; }
        public Entry Entry { get; set; }

    }
}
