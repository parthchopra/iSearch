using System;
using iSearch.Models;

namespace iSearch.Repositories
{
	public class CollectionRepository : ICollectionRepository
	{
		private readonly CollectionDbContext _collectionDbContext;
		public CollectionRepository(CollectionDbContext context)
		{
            _collectionDbContext = context;
		}        

        public async Task AddClickCountAsync(long collectionId)
        {
            var collection = _collectionDbContext.CollectionEntities.SingleOrDefault(c => c.CollectionId == collectionId);
            if(collection == null)
            {
                await _collectionDbContext.CollectionEntities.AddAsync(new CollectionEntity { CollectionId = collectionId, ClickCount = 1 });
            }
            else
            {
                collection.ClickCount++;
            }

            await _collectionDbContext.SaveChangesAsync();
        }

        public IEnumerable<SingleCollectionViewModel> GetCollectionsWithClickCounts(IEnumerable<Collection> collections)
        {
            var collectionEntities = _collectionDbContext.CollectionEntities.Select(ce => ce);
            var singleCollections  = collections.GroupJoin(
                collectionEntities,
                collection => collection.CollectionId,
                collectionEntity => collectionEntity.CollectionId,
                (collection, collectionEntities) => new { collection, collectionEntities }
                ).SelectMany(x => x.collectionEntities.DefaultIfEmpty(),
                    (collection, collectionEntity) => new SingleCollectionViewModel(
                            collectionId: collection.collection.CollectionId,
                            collectionName: collection.collection.CollectionName,
                            collectionViewUrl: collection.collection.CollectionViewUrl,
                            artworkUrl60: collection.collection.ArtworkUrl60,
                            artworkUrl100: collection.collection.ArtworkUrl100,
                            clickCount: collectionEntity?.ClickCount ?? 0
                        )
                    );

            return singleCollections;
        }

    }
}

