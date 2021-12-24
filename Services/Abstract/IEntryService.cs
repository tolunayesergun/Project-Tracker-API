using ProjectTracker_API.Models.Concretes;
using ProjectTracker_API.Models.DTOs;
using System.Threading.Tasks;

namespace EntryTracker.Services.Abstract
{
    public interface IEntryService
    {
        Task<ServiceResponse<int>> CreateEntry(EntryDTO entryDTO, int userId);

        Task<ServiceResponse> DeleteEntry(int entryId, int userId);

        Task<ServiceResponse> UpdateEntry(EntryDTO entryDTO, int entryId, int userId);

        Task<ServiceResponse<EntryDTO>> GetEntry(int entryId, int userId);
    }
}
