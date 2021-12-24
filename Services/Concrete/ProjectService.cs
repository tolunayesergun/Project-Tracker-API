using AutoMapper;
using ProjectTracker_API.DataAccess.DataAccessLayers.Abstract;
using ProjectTracker_API.Helpers;
using ProjectTracker_API.Models.Concretes;
using ProjectTracker_API.Models.Constants;
using ProjectTracker_API.Models.DTOs;
using ProjectTracker_API.Models.Entities;
using ProjectTracker_API.Services.Abstract;
using System.Threading.Tasks;

namespace ProjectTracker_API.Services.Concrete
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectDAL _projectDAL;
        private readonly IMapper _mapper;
        private const int retryCreateCodeCount = 50; // Appsettings Taşınıcak

        public ProjectService(IProjectDAL projectDAL, IMapper mapper)
        {
            _projectDAL = projectDAL;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> CreateProject(ProjectDTO projectDTO, int userId)
        {
            var mappedProject = _mapper.Map<Project>(projectDTO);
            mappedProject = PropertyHelper<Project>.FillPropForUser(mappedProject, true, userId);
            mappedProject.ProjectCode = GetProjectCode();
            if (mappedProject.ProjectCode == string.Empty) return Responses<int>.FailedResponse();
            var projectId = await _projectDAL.AddAsyncReturnId(mappedProject);
            return Responses<int>.SuccessResponse(projectId);
        }

        public async Task<ServiceResponse> DeleteProject(int projectId, int userId)
        {
            var project = await _projectDAL.GetAsync(x => x.Id == projectId);
            await _projectDAL.DeleteAsync(project);

            return Responses.SuccessResponse();
        }

        public async Task<ServiceResponse<ProjectDTO>> GetProject(int projectId, int userId)
        {
            var project = await _projectDAL.GetAsync(x => x.Id == projectId);

            return Responses<ProjectDTO>.SuccessResponse(_mapper.Map<ProjectDTO>(project));
        }

        public async Task<ServiceResponse> UpdateProject(ProjectDTO projectDTO, int projectId, int userId)
        {
            var project = await _projectDAL.GetAsync(x => x.Id == projectId);
            var updatedProject = _mapper.Map<Project>(projectDTO);
            updatedProject = PropertyHelper<Project>.CompareShareds(project, updatedProject);
            await _projectDAL.UpdateAsync(updatedProject);
            return Responses.SuccessResponse();
        }


        private string GetProjectCode()
        {
            var projectCode = string.Empty;
            for (int i = 0; i < retryCreateCodeCount; i++)
            {
                projectCode = CammonHelper.RandomCode(2, 5);
                if (_projectDAL.Get(x => x.ProjectCode == projectCode) is null) break;

            }
            return projectCode;
        }
    }
}
