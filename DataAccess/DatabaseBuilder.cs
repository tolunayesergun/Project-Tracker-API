using Microsoft.EntityFrameworkCore;
using System;

namespace ProjectTracker_API.DataAccess
{
    public class DatabaseBuilder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DataBaseContext();
            context.Database.Migrate();
        }
    }
}