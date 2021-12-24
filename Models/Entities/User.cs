using ProjectTracker_API.Models.Concretes;

namespace ProjectTracker_API.Models.Entities
{
    public class User:SharedProperties
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
