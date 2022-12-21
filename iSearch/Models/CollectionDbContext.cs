using System;
using Microsoft.EntityFrameworkCore;

namespace iSearch.Models
{
	public class CollectionDbContext : DbContext
	{
		public CollectionDbContext(DbContextOptions<CollectionDbContext> options) : base(options)
		{
		}

		public DbSet<CollectionEntity> CollectionEntities { get; set; }
	}
}

