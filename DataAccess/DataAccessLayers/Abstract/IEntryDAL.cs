using ProjectTracker_API.DataAccess.RepositoryBases.Abstract;
using ProjectTracker_API.Models.Entities;

namespace ProjectTracker_API.DataAccess.DataAccessLayers.Abstract
{
    public interface IEntryDAL : IGenericRepository<Entry>
    {
    }
}
