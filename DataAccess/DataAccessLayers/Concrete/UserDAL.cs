using Microsoft.EntityFrameworkCore;
using ProjectTracker_API.DataAccess.DataAccessLayers.Abstract;
using ProjectTracker_API.DataAccess.RepositoryBases.Concrete;
using ProjectTracker_API.Models.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectTracker_API.DataAccess.DataAccessLayers.Concrete
{
    public class UserDAL : GenericRepository<User>, IUserDAL
    {
        public async Task<bool> CheckUser(Expression<Func<User, bool>> filter)
        {
            using var context = new DataBaseContext();
            return context.Users is null ? throw new ArgumentNullException() : await context.Users.AnyAsync(filter);
        }
    }
}