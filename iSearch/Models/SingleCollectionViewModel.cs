namespace iSearch.Models
{
    public class SingleCollectionViewModel
	{
        public long CollectionId { get; set; }
        public string CollectionName { get; set; } = string.Empty;
        public string CollectionViewUrl { get; set; } = string.Empty;
        public string? ArtworkUrl60 { get; set; }
        public string? ArtworkUrl100 { get; set; }
        public long ClickCount { get; set; } = 0;

        public SingleCollectionViewModel(long collectionId
            , string collectionName
            , string collectionViewUrl
            , string? artworkUrl60 = null
            , string? artworkUrl100 = null
            , long clickCount = 0)
        {
            CollectionId = collectionId;
            CollectionName = collectionName;
            CollectionViewUrl = collectionViewUrl;
            ArtworkUrl60 = artworkUrl60;
            ArtworkUrl100 = artworkUrl100;
            ClickCount = clickCount;
        }
    }
}

 