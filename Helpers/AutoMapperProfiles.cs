using AutoMapper;
using ProjectTracker_API.Models.DTOs;
using ProjectTracker_API.Models.DTOs.UserDTOs;
using ProjectTracker_API.Models.Entities;

namespace TeamExpenseAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDTO, User>().ReverseMap();
            CreateMap<LoginDTO, RegisterDTO>().ReverseMap();
            CreateMap<ProjectDTO, Project>().ReverseMap();
            CreateMap<EntryDTO, Entry>().ReverseMap();
        }
    }
}