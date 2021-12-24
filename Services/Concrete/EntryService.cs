using AutoMapper;
using EntryTracker.Services.Abstract;
using ProjectTracker_API.DataAccess.DataAccessLayers.Abstract;
using ProjectTracker_API.Helpers;
using ProjectTracker_API.Models.Concretes;
using ProjectTracker_API.Models.Constants;
using ProjectTracker_API.Models.DTOs;
using ProjectTracker_API.Models.Entities;
using System.Threading.Tasks;

namespace EntryTracker.Services.Concrete
{
    public class EntryService: IEntryService
    {
        private readonly IEntryDAL _entryDAL;
        private readonly IMapper _mapper;

        public EntryService(IEntryDAL entryDAL, IMapper mapper)
        {
            _entryDAL = entryDAL;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> CreateEntry(EntryDTO entryDTO, int userId)
        {
            var mappedEntry = _mapper.Map<Entry>(entryDTO);
            mappedEntry = PropertyHelper<Entry>.FillPropForUser(mappedEntry, true, userId);
            var entryId = await _entryDAL.AddAsyncReturnId(mappedEntry);
            return Responses<int>.SuccessResponse(entryId);
        }

        public async Task<ServiceResponse> DeleteEntry(int entryId, int userId)
        {
            var entry = await _entryDAL.GetAsync(x => x.Id == entryId);
            await _entryDAL.DeleteAsync(entry);

            return Responses.SuccessResponse();
        }

        public async Task<ServiceResponse<EntryDTO>> GetEntry(int entryId, int userId)
        {
            var entry = await _entryDAL.GetAsync(x => x.Id == entryId);

            return Responses<EntryDTO>.SuccessResponse(_mapper.Map<EntryDTO>(entry));
        }

        public async Task<ServiceResponse> UpdateEntry(EntryDTO entryDTO, int entryId, int userId)
        {
            var entry = await _entryDAL.GetAsync(x => x.Id == entryId);
            var updatedEntry = _mapper.Map<Entry>(entryDTO);
            updatedEntry = PropertyHelper<Entry>.CompareShareds(entry, updatedEntry);
            await _entryDAL.UpdateAsync(updatedEntry);
            return Responses.SuccessResponse();
        }


    }
}
