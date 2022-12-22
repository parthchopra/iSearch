using System;
using iSearch.Models;
using iSearch.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
namespace iSearch.Tests.Repositories
{
    [Trait("Category", "IntegrationTests")]
	public class CollectionRepositoryTests : IDisposable
	{

        private const string ConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection connection;
        private readonly CollectionDbContext context;
        private readonly IEnumerable<Collection> collectionsStub;

        public CollectionRepositoryTests()
		{
            connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<CollectionDbContext>()
            .UseSqlite(connection)
            .Options;

            context = new CollectionDbContext(contextOptions);
            context.Database.EnsureCreated();

            collectionsStub = new List<Collection>()
            {
                new Collection {CollectionId = 1, CollectionName = "test_collection_1" },
                new Collection {CollectionId = 2, CollectionName = "test_collection_2" },
                new Collection {CollectionId = 3, CollectionName = "test_collection_3" },
                new Collection {CollectionId = 4, CollectionName = "test_collection_4" },
            };
        }

        public void Dispose()
        {
            connection.Close();
        }

        [Fact]
		public void GetCollectionWithClickAndEmptyDB_ReturnsCollectionsWithSearchCountSetTo0()
		{
            var repo = new CollectionRepository(context);
            var actualSingleCollections = repo.GetCollectionsWithClickCounts(collectionsStub);

            Assert.Equal(4, actualSingleCollections.Count());
            Assert.Equal(0, actualSingleCollections.First().ClickCount);
            Assert.Equal(0, actualSingleCollections.Last().ClickCount);
        }

        [Fact]
        public void GetCollectionWithClickAppendsClickCountsFromDatabase()
        {
            context.CollectionEntities.AddRange(
                new CollectionEntity
                {
                    CollectionId = 1,
                    ClickCount = 2
                },
                new CollectionEntity
                {
                    CollectionId = 2,
                    ClickCount = 1
                }
            );

            context.SaveChanges();

            var repo = new CollectionRepository(context);
            var actualSingleCollections = repo.GetCollectionsWithClickCounts(collectionsStub);

            Assert.Equal(4, actualSingleCollections.Count());
            Assert.Equal(2, actualSingleCollections.First(c => c.CollectionId == 1).ClickCount);
            Assert.Equal(1, actualSingleCollections.First(c => c.CollectionId == 2).ClickCount);
            Assert.Equal(0, actualSingleCollections.First(c => c.CollectionId == 3).ClickCount);
        }

        [Fact]
        public async Task AddClickCountAsyncAddsClickCount()
        {
            await context.CollectionEntities.AddRangeAsync(
                new CollectionEntity
                {
                    CollectionId = 1,
                    ClickCount = 2
                },
                new CollectionEntity
                {
                    CollectionId = 2,
                    ClickCount = 1
                }
            );

            await context.SaveChangesAsync();

            var repo = new CollectionRepository(context);
            await repo.AddClickCountAsync(1);

            Assert.Equal(3, context.CollectionEntities.First(c => c.CollectionId == 1).ClickCount);
        }
	}
}