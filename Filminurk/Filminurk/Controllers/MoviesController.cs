using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        private readonly IFileServices _filesServices;
        public MoviesController ( FilminurkTARpe24Context context, IMovieServices movieServices, IFileServices fileServices )
        {
            _context = context;
            _movieServices = movieServices;
            _filesServices = fileServices;
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
                genre = x.genre,
            });
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateUpdateViewModel vm)
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
                genre = vm.genre,
                Files = vm.Files,
                FileToApiDtos = vm.Images.Select(x => new FileToApiDto
                {
                    ImageID = x.ImageID,
                    FilePath = x.FilePath,
                    MovieID = x.MovieID,
                    IsPoster = x.IsPoster,
                })

            };
            var result = await _movieServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var movie = await _movieServices.DetailsAsync(id);

        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    var images = await _context.FilesToApi.Where(x => x.MovieID == id).Select(y => new ImageViewModel
        //    {
        //        FilePath = y.ExistingFilePath,
        //        ImageID = id
        //    }).ToArrayAsync();

        //    var vm = new ImageViewModel();
        //    vm.IsPoster = vm.IsPoster;
        //    vm.Title = movie.Title;
        //    vm.Description = movie.Description;
        //    vm.FirstPublished = movie.FirstPublished;
        //    vm.CurrentRatting = movie.CurrentRatting;
        //    vm.MovieCreationCost = movie.MovieCreationCost;
        //    vm.Studio = movie.Studio;
        //    vm.genre = movie.genre;
        //    vm.EntryCreatedAt = movie.EntryCreatedAt;
        //    vm.EntryModifiedAt = movie.EntryModifiedAt;
        //    vm.Director = movie.Director;
        //    vm.Actors = movie.Actors;
        //    vm.Images.AddRange(images);

        //    return View("CreateUpdate", vm);
        //}

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToApi.Where(x => x.MovieID == id).Select(y => new ImageViewModel
            {
                FilePath = y.ExistingFilePath,
                ImageID = id
            }).ToArrayAsync();

            var vm = new MoviesCreateUpdateViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRatting = movie.CurrentRatting;
            vm.MovieCreationCost = movie.MovieCreationCost;
            vm.Studio = movie.Studio;
            vm.genre = movie.genre;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.Images.AddRange(images);

            return View("CreateUpdate",vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MoviesCreateUpdateViewModel vm)
        {
            var dto = new MoviesDto()
            {
                ID = vm.ID,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                CurrentRatting = vm.CurrentRatting,
                Director = vm.Director,
                Actors = vm.Actors,
                MovieCreationCost = vm.MovieCreationCost,
                Studio = vm.Studio,
                genre = vm.genre,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt,
                Files = vm.Files,
                FileToApiDtos = vm.Images.Select(x => new FileToApiDto
                {
                    MovieID = x.MovieID,
                    FilePath = x.FilePath,
                    ImageID = x.ImageID,
                }).ToArray()
            };

            var result = _movieServices.Update(dto);

            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movies = await _movieServices.DetailsAsync(id);
            if (movies == null)
            {
                return NotFound();
            }

            var images = await _context.FilesToApi.Where(x  => x.MovieID == id).Select(y => new ImageViewModel
            {
                FilePath = y.ExistingFilePath,
                ImageID = y.ImageID,
            }).ToArrayAsync();

            var vm = new MoviesCreateUpdateViewModel();
            vm.ID = movies.ID;
            vm.Title = movies.Title;
            vm.Description = movies.Description;
            vm.FirstPublished = movies.FirstPublished;
            vm.CurrentRatting = movies.CurrentRatting;
            vm.Director = movies.Director;
            vm.Actors = movies.Actors;
            vm.MovieCreationCost = movies.MovieCreationCost;
            vm.Studio = movies.Studio;
            vm.genre = movies.genre;
            vm.EntryCreatedAt = movies.EntryCreatedAt;
            vm.EntryModifiedAt = movies.EntryModifiedAt;
            vm.Images.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var movies = await _movieServices.Delete(id);
            if (movies == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task<ImageViewModel[]> FileFromDatabase(Guid id)
        {
            return await _context.FilesToApi.Where(x => x.MovieID == id).Select(y => new ImageViewModel
            {
                ImageID = y.ImageID,
                MovieID = y.MovieID,
                IsPoster = y.IsPoster,
                FilePath = y.ExistingFilePath
            }).ToArrayAsync();
        }
    }
}
