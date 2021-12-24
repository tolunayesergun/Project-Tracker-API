using ProjectTracker_API.Models.Concretes;
using ProjectTracker_API.Models.DTOs.UserDTOs;
using System.Threading.Tasks;

namespace ProjectTracker_API.Services.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> LoginAsync(LoginDTO user);

        Task<ServiceResponse<string>> RegisterAsync(RegisterDTO user);
    }
}