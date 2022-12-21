using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using iSearch.Models;
using iSearch.Services;
using iSearch.Repositories;
using NuGet.Packaging;

namespace iSearch.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITunesSearchService _tunesSearchService;
    private readonly ICollectionRepository _collectionRepository;

    public HomeController(ILogger<HomeController> logger, ITunesSearchService tunesSearchService, ICollectionRepository collectionRepository)
    {
        _logger = logger;
        _tunesSearchService = tunesSearchService;
        _collectionRepository = collectionRepository;
    }

    public IActionResult Index()
    {
        return View(new CollectionViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(CollectionViewModel collectionViewModel)
    {

        if (ModelState.IsValid)
        {
            var searchResult = await _tunesSearchService.SearchAsync(collectionViewModel.SearchQuery);

            if (searchResult.ResultCount > 0)
            {
                collectionViewModel.Collections = _collectionRepository.GetCollectionsWithClickCounts(searchResult.Results);
            }
        }


        return View(collectionViewModel);
    }

    public async Task<IActionResult> VisitCollection(long id, string url)
    {
        await _collectionRepository.AddClickCountAsync(id);
        return Redirect(url);
    }    

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

