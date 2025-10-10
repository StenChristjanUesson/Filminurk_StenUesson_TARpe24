using Filminurk.Core.Dto;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public MoviesController ( FilminurkTARpe24Context context )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID = x.ID,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                Description = x.Description,
                CurrentRatting = x.CurrentRatting,
                MovieCreationCost = x.MovieCreationCost,
                Studio = x.Studio,
                genre = (Genre)x.genre,
            });
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateViewModel result = new();
            return View("Create", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateViewModel vm)
        {
            var dto = new MoviesDto()
            {
                ID = vm.ID,
                Title = vm.Title,
                FirstPublished = vm.FirstPublished,
                Director = vm.Director,
                Description = vm.Description,
                Actors = vm.Actors,
                CurrentRatting = vm.CurrentRatting,
                MovieCreationCost = vm.MovieCreationCost,
                Studio = vm.Studio,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt,
                genre = (Core.Dto.Genre?)vm.genre,
            };
            var result = await _context.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
