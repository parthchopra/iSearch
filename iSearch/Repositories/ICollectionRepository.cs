
using iSearch.Models;

namespace iSearch.Repositories
{
	public interface ICollectionRepository
	{
		public Task AddClickCountAsync(long collectionId);
		public IEnumerable<SingleCollectionViewModel> GetCollectionsWithClickCounts(IEnumerable<Collection> collections);
    }
}

