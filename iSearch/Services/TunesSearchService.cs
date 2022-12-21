using System;
using iSearch.Models;

namespace iSearch.Services
{
	public class TunesSearchService : ITunesSearchService
	{
		private readonly string iTunesSearchUrlBase = "https://itunes.apple.com/search?term=";
        private readonly HttpClient _httpClient;

        public TunesSearchService(HttpClient httpClient)
		{
            _httpClient = httpClient;
		}

        public async Task<SearchResult> SearchAsync(string searchParameter)
        {
            SearchResult? searchResult = null;
            var result = await _httpClient.GetAsync($"{iTunesSearchUrlBase}{searchParameter}");

            if (result.IsSuccessStatusCode)
            {
                searchResult = await result.Content.ReadFromJsonAsync<SearchResult>() ?? null;
            }
            else
            {
                var message = await result.Content.ReadAsStringAsync();
                throw new Exception(message);
            }

            if(searchResult == null)
            {
                throw new Exception("unable to deserialize search result");
            }
            return searchResult;
        }
    }
}

