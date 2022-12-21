using System;
using iSearch.Models;

namespace iSearch.Services
{
	public interface ITunesSearchService
	{
		public Task<SearchResult> SearchAsync(string searchParameter);
	}
}

