namespace iSearch.Models
{
	public class SearchResult
	{
		public int ResultCount { get; set; }

		public IEnumerable<Collection> Results { get; set; } = new List<Collection>();
	}
}

