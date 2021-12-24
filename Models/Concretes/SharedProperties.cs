using ProjectTracker_API.Models.Abstracts;
using System;

namespace ProjectTracker_API.Models.Concretes
{
    public class SharedProperties : IEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CreateUserID { get; set; }
        public int UpdateUserID { get; set; }
        public int Status { get; set; }
    }
}