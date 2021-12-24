using ProjectTracker_API.Models.Concretes;

namespace ProjectTracker_API.Models.Entities
{
    public class ProjectRequest:SharedProperties
    {
        public string RequestMessage { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int JoinerUserId { get; set; }
        public  User JoinerUser { get; set; }
        public int ApproverUserId { get; set; }
        public  User ApproverUser { get; set; }
    }
}
