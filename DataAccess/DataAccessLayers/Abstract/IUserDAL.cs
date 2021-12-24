using ProjectTracker_API.DataAccess.RepositoryBases.Abstract;
using ProjectTracker_API.Models.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectTracker_API.DataAccess.DataAccessLayers.Abstract
{
    public interface IUserDAL : IGenericRepository<User>
    {
        Task<bool> CheckUser(Expression<Func<User, bool>> filter);
    }
}