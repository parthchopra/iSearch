using System;
namespace iSearch.Models
{
	public class Collection
	{
        public long CollectionId { get; set; }
        public string CollectionName { get; set; } = string.Empty;
        public string CollectionViewUrl { get; set; } = string.Empty;
        public string? ArtworkUrl60 { get; set; }
        public string? ArtworkUrl100 { get; set; }


    }
}

