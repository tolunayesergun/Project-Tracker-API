using ProjectTracker_API.DataAccess.DataAccessLayers.Abstract;
using ProjectTracker_API.DataAccess.RepositoryBases.Concrete;
using ProjectTracker_API.Models.Entities;

namespace ProjectTracker_API.DataAccess.DataAccessLayers.Concrete
{
    public class EntryDAL : GenericRepository<Entry>, IEntryDAL
    {
    }
}
