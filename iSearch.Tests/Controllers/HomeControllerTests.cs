using System;
using iSearch.Controllers;
using iSearch.Models;
using iSearch.Repositories;
using iSearch.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace iSearch.Tests.Controllers
{
	public class HomeControllerTests
	{
		private readonly Mock<ICollectionRepository> mockCollectionRepository;
		private readonly Mock<ITunesSearchService> mockTunesSearchService;
		private readonly ILogger<HomeController> logger;		 

		public HomeControllerTests()
		{
			mockCollectionRepository = new Mock<ICollectionRepository>();
			mockTunesSearchService = new Mock<ITunesSearchService>();
            logger = new Mock<ILogger<HomeController>>().Object;
        }

		[Fact]
		public void IndexWithNoParamsSendsEmptyViewModel()
		{
			var controller = new HomeController(logger, mockTunesSearchService.Object, mockCollectionRepository.Object);

			var result = controller.Index();

			var viewResult = Assert.IsType<ViewResult>(result);
			var collectionVM = Assert.IsAssignableFrom<CollectionViewModel>(viewResult.ViewData.Model);
			Assert.Empty(collectionVM.SearchQuery);			
		}

		[Fact]
		public async Task VisitCollectionAddsClickCountAndRedirectsAsync()
		{
            var controller = new HomeController(logger, mockTunesSearchService.Object, mockCollectionRepository.Object);

			var redirectUrl = "testUrl";
            var result = await controller.VisitCollection(1, redirectUrl);

			var redirectResult = Assert.IsType<RedirectResult>(result);
			Assert.Equal(redirectUrl, redirectResult.Url);
			mockCollectionRepository.Verify(x => x.AddClickCountAsync(1), Times.Once);
        }
	}
}

