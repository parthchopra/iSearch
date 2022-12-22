using System;
using System.Net;
using iSearch.Services;
using Moq;
using Moq.Protected;

namespace iSearch.Tests.Services
{
    public class TunesSearchServiceTests
    {
        private string responseJson = """
            {
               "resultCount":4,
               "results":[
                  {
                     "wrapperType":"track",
                     "kind":"song",
                     "artistId":62763238,
                     "collectionId":1011505963,
                     "trackId":1011505965,
                     "artistName":"Yo Gotti",
                     "collectionName":"Rihanna (feat. Young Thug) - Single",
                     "trackName":"Rihanna (feat. Young Thug)",
                     "collectionCensoredName":"Rihanna (feat. Young Thug) - Single",
                     "trackCensoredName":"Rihanna (feat. Young Thug)",
                     "artistViewUrl":"https://music.apple.com/us/artist/yo-gotti/62763238?uo=4",
                     "collectionViewUrl":"https://music.apple.com/us/album/rihanna-feat-young-thug/1011505963?i=1011505965&uo=4",
                     "trackViewUrl":"https://music.apple.com/us/album/rihanna-feat-young-thug/1011505963?i=1011505965&uo=4",
                     "previewUrl":"https://audio-ssl.itunes.apple.com/itunes-assets/AudioPreview115/v4/01/ca/1d/01ca1d8c-cb5a-62ea-430f-afd7ffb17dd6/mzaf_4207205340904194295.plus.aac.p.m4a",
                     "artworkUrl30":"https://is5-ssl.mzstatic.com/image/thumb/Music1/v4/ec/7b/75/ec7b7587-edd6-75e5-20ba-28c46339deb0/886445318869.jpg/30x30bb.jpg",
                     "artworkUrl60":"https://is5-ssl.mzstatic.com/image/thumb/Music1/v4/ec/7b/75/ec7b7587-edd6-75e5-20ba-28c46339deb0/886445318869.jpg/60x60bb.jpg",
                     "artworkUrl100":"https://is5-ssl.mzstatic.com/image/thumb/Music1/v4/ec/7b/75/ec7b7587-edd6-75e5-20ba-28c46339deb0/886445318869.jpg/100x100bb.jpg",
                     "collectionPrice":1.29,
                     "trackPrice":1.29,
                     "releaseDate":"2015-06-29T07:00:00Z",
                     "collectionExplicitness":"explicit",
                     "trackExplicitness":"explicit",
                     "discCount":1,
                     "discNumber":1,
                     "trackCount":1,
                     "trackNumber":1,
                     "trackTimeMillis":234459,
                     "country":"USA",
                     "currency":"USD",
                     "primaryGenreName":"Hip-Hop/Rap",
                     "contentAdvisoryRating":"Explicit",
                     "isStreamable":true
                  },
                  {
                     "wrapperType":"track",
                     "kind":"song",
                     "artistId":111051,
                     "collectionId":1517894593,
                     "trackId":1517894609,
                     "artistName":"Eminem",
                     "collectionName":"Recovery (Deluxe Edition)",
                     "trackName":"Love the Way You Lie (feat. Rihanna)",
                     "collectionCensoredName":"Recovery (Deluxe Edition)",
                     "trackCensoredName":"Love the Way You Lie (feat. Rihanna)",
                     "artistViewUrl":"https://music.apple.com/us/artist/eminem/111051?uo=4",
                     "collectionViewUrl":"https://music.apple.com/us/album/love-the-way-you-lie-feat-rihanna/1517894593?i=1517894609&uo=4",
                     "trackViewUrl":"https://music.apple.com/us/album/love-the-way-you-lie-feat-rihanna/1517894593?i=1517894609&uo=4",
                     "previewUrl":"https://audio-ssl.itunes.apple.com/itunes-assets/AudioPreview115/v4/0c/99/f3/0c99f3e5-6513-b782-547b-95905a95c219/mzaf_7328992109900298721.plus.aac.p.m4a",
                     "artworkUrl30":"https://is1-ssl.mzstatic.com/image/thumb/Music125/v4/6a/da/5f/6ada5fd5-5e05-20bc-4bb0-6c0f1f0d91cd/10UMGIM14533.rgb.jpg/30x30bb.jpg",
                     "artworkUrl60":"https://is1-ssl.mzstatic.com/image/thumb/Music125/v4/6a/da/5f/6ada5fd5-5e05-20bc-4bb0-6c0f1f0d91cd/10UMGIM14533.rgb.jpg/60x60bb.jpg",
                     "artworkUrl100":"https://is1-ssl.mzstatic.com/image/thumb/Music125/v4/6a/da/5f/6ada5fd5-5e05-20bc-4bb0-6c0f1f0d91cd/10UMGIM14533.rgb.jpg/100x100bb.jpg",
                     "collectionPrice":11.99,
                     "trackPrice":1.29,
                     "releaseDate":"2010-06-18T07:00:00Z",
                     "collectionExplicitness":"explicit",
                     "trackExplicitness":"explicit",
                     "discCount":1,
                     "discNumber":1,
                     "trackCount":19,
                     "trackNumber":15,
                     "trackTimeMillis":263373,
                     "country":"USA",
                     "currency":"USD",
                     "primaryGenreName":"Hip-Hop/Rap",
                     "contentAdvisoryRating":"Explicit",
                     "isStreamable":true
                  },
                  {
                     "wrapperType":"track",
                     "kind":"song",
                     "artistId":111051,
                     "collectionId":1440862963,
                     "trackId":1440863108,
                     "artistName":"Eminem",
                     "collectionName":"The Marshall Mathers LP2 (Deluxe)",
                     "trackName":"The Monster (feat. Rihanna)",
                     "collectionCensoredName":"The Marshall Mathers LP2 (Deluxe)",
                     "trackCensoredName":"The Monster (feat. Rihanna)",
                     "artistViewUrl":"https://music.apple.com/us/artist/eminem/111051?uo=4",
                     "collectionViewUrl":"https://music.apple.com/us/album/the-monster-feat-rihanna/1440862963?i=1440863108&uo=4",
                     "trackViewUrl":"https://music.apple.com/us/album/the-monster-feat-rihanna/1440862963?i=1440863108&uo=4",
                     "previewUrl":"https://audio-ssl.itunes.apple.com/itunes-assets/AudioPreview115/v4/c5/4b/3e/c54b3ee2-c7ab-c585-9a27-48add4ba64cb/mzaf_14924300837198509883.plus.aac.p.m4a",
                     "artworkUrl30":"https://is3-ssl.mzstatic.com/image/thumb/Music125/v4/c7/d2/78/c7d2787f-5975-9263-fe47-eeebee3c892f/00602537542703.rgb.jpg/30x30bb.jpg",
                     "artworkUrl60":"https://is3-ssl.mzstatic.com/image/thumb/Music125/v4/c7/d2/78/c7d2787f-5975-9263-fe47-eeebee3c892f/00602537542703.rgb.jpg/60x60bb.jpg",
                     "artworkUrl100":"https://is3-ssl.mzstatic.com/image/thumb/Music125/v4/c7/d2/78/c7d2787f-5975-9263-fe47-eeebee3c892f/00602537542703.rgb.jpg/100x100bb.jpg",
                     "collectionPrice":11.99,
                     "trackPrice":1.29,
                     "releaseDate":"2013-10-29T12:00:00Z",
                     "collectionExplicitness":"explicit",
                     "trackExplicitness":"explicit",
                     "discCount":1,
                     "discNumber":1,
                     "trackCount":21,
                     "trackNumber":12,
                     "trackTimeMillis":250189,
                     "country":"USA",
                     "currency":"USD",
                     "primaryGenreName":"Hip-Hop/Rap",
                     "contentAdvisoryRating":"Explicit",
                     "isStreamable":true
                  },
                  {
                     "wrapperType":"track",
                     "kind":"song",
                     "artistId":14967,
                     "collectionId":1260880949,
                     "trackId":1260881180,
                     "artistName":"T.I.",
                     "collectionName":"Paper Trail (Deluxe Version)",
                     "trackName":"Live Your Life (feat. Rihanna)",
                     "collectionCensoredName":"Paper Trail (Deluxe Version)",
                     "trackCensoredName":"Live Your Life (feat. Rihanna)",
                     "artistViewUrl":"https://music.apple.com/us/artist/t-i/14967?uo=4",
                     "collectionViewUrl":"https://music.apple.com/us/album/live-your-life-feat-rihanna/1260880949?i=1260881180&uo=4",
                     "trackViewUrl":"https://music.apple.com/us/album/live-your-life-feat-rihanna/1260880949?i=1260881180&uo=4",
                     "previewUrl":"https://audio-ssl.itunes.apple.com/itunes-assets/AudioPreview125/v4/60/66/7a/60667af5-61ac-388b-04eb-3dfbf82e1fd2/mzaf_4607312024548255594.plus.aac.p.m4a",
                     "artworkUrl30":"https://is4-ssl.mzstatic.com/image/thumb/Music125/v4/92/24/93/922493d6-278f-d6aa-99c6-e0cf0de3d17f/050742335373_art.jpg/30x30bb.jpg",
                     "artworkUrl60":"https://is4-ssl.mzstatic.com/image/thumb/Music125/v4/92/24/93/922493d6-278f-d6aa-99c6-e0cf0de3d17f/050742335373_art.jpg/60x60bb.jpg",
                     "artworkUrl100":"https://is4-ssl.mzstatic.com/image/thumb/Music125/v4/92/24/93/922493d6-278f-d6aa-99c6-e0cf0de3d17f/050742335373_art.jpg/100x100bb.jpg",
                     "collectionPrice":13.99,
                     "trackPrice":1.29,
                     "releaseDate":"2008-09-23T12:00:00Z",
                     "collectionExplicitness":"explicit",
                     "trackExplicitness":"explicit",
                     "discCount":1,
                     "discNumber":1,
                     "trackCount":18,
                     "trackNumber":5,
                     "trackTimeMillis":338853,
                     "country":"USA",
                     "currency":"USD",
                     "primaryGenreName":"Rap",
                     "contentAdvisoryRating":"Explicit",
                     "isStreamable":true
                  }
               ]
            }
            """;
        private Mock<HttpMessageHandler> handlerMock;
        private HttpClient client;

        public TunesSearchServiceTests()
        {
            handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseJson)
                })
                .Verifiable();
            client = new HttpClient(handlerMock.Object);
        }

        [Fact]
        public async Task SearchItunesAsync_ReturnsSearchResultObject()
        {                        
            var tunesSearchService = new TunesSearchService(client);

            var actualResult = await tunesSearchService.SearchAsync("test");

            Assert.NotNull(actualResult);
            Assert.Equal(4, actualResult.Results.Count());
        }
    }
}

