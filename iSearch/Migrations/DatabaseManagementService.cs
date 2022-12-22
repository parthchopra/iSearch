using System;
using iSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace iSearch.Migrations
{
	public static class DatabaseManagementService
	{
        public static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Takes all of our migrations files and apply them against the database in case they are not implemented
                serviceScope.ServiceProvider.GetService<CollectionDbContext>()?.Database.Migrate();
            }
        }
    }
}

