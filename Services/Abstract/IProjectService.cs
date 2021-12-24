using ProjectTracker_API.Models.Concretes;
using ProjectTracker_API.Models.DTOs;
using System.Threading.Tasks;

namespace ProjectTracker_API.Services.Abstract
{
    public interface IProjectService
    {
        Task<ServiceResponse<int>> CreateProject(ProjectDTO projectDTO, int userId);

        Task<ServiceResponse> DeleteProject(int projectId, int userId);

        Task<ServiceResponse> UpdateProject(ProjectDTO projectDTO, int projectId, int userId);

        Task<ServiceResponse<ProjectDTO>> GetProject(int projectId, int userId);
    }
}
