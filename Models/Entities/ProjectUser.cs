using ProjectTracker_API.Models.Concretes;

namespace ProjectTracker_API.Models.Entities
{
    public class ProjectUser: SharedProperties
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        /// <summary>
        /// [ Owner = 0 ]-[ Manager = 1 ]-[ User = 2 ]
        /// </summary>
        public int ProjectUserRole { get; set; }
  
    }
}
