using Filminurk.ApplicationServices.Services;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.FavouritesLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouritesListController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavouriteListsServices _favouriteListsServices;
        // fileservice add later
        public FavouritesListController(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.favouriteLists
                .OrderByDescending(y => y.ListCreatedAt)
                .Select(x => new FavouritesListsIndexViewModel
                {
                    FavouriteListID = x.FavouriteListID,
                    ListName = x.ListName,
                    ListBelongsToUser = x.ListBelongsToUser,
                    IsMovieOrActor = x.IsMovieOrActor,
                    ListDescription = x.ListDescription,
                    ListCreatedAt = x.ListCreatedAt,
                    Image = (List<FavouriteListIndexImageViewModel>)_context.FileToDatabase
                .Where(ml => ml.ListID == x.FavouriteListID)
                .Select(li => new FavouriteListIndexImageViewModel
                {
                    ListID = li.ListID,
                    ImageID = li.ImageID,
                    ImageData = li.ImageData,
                    ImageTitle = li.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(li.ImageData)),
                })
                });
            return View(resultingLists);
        }
        /* create get, create post */
        [HttpGet]
        public IActionResult Create()
        {
            var movies = _context.Movies.OrderBy(m => m.Title).Select(mo => new MoviesIndexViewModel
            {
                ID = mo.ID,
                Title = mo.Title,
                CurrentRatting = mo.CurrentRatting,
                FirstPublished = mo.FirstPublished,
                genre = (Genre)mo.genre,
            }).ToList();
            ViewData["allmovies"] = movies;
            ViewData["userHasSelected"] = new List<string>();
            //TODO IDENTIFY USER
            FavoriteListUserCreateViewModel vm = new();
            return View("UserCreate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(FavoriteListUserCreateViewModel vm, List<string> userHasSelected, List<MoviesIndexViewModel> movies)
        {
            List<Guid> tempParse = new();
            foreach (var stringID in userHasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));
            }
            var newListDto = new FavouriteListDto() { };
            newListDto.ListName = vm.ListName;
            newListDto.ListDescription = vm.Description;
            newListDto.IsMovieOrActor = vm.IsMovieOrActor;
            newListDto.IsPrivate = vm.IsPrivate;
            newListDto.ListCreatedAt = DateTime.UtcNow;
            newListDto.ListBelongsToUser = "00000000-0000-0000-0000-000000000001";
            newListDto.ListModifiedAt = DateTime.UtcNow;
            newListDto.ListDeletedAt = vm.ListDeletedAt;

            var listofmoviestoadd = new List<Movie>();
            foreach (var movieId in tempParse)
            {
                var thismovie = _context.Movies.Where(tm => tm.ID == movieId).ToArray().Take(1);
                listofmoviestoadd.Add((Movie)thismovie);
            }
            newListDto.ListOfMovies = listofmoviestoadd;

            /*
            List<Guid> convertedIDs = new List<Guid>();
            if (newListDto.ListOfMovies != null)
            {
                convertedIDs = MovieToId(newListDto.ListOfMovies);
            }
            */
            var newList = await _favouriteListsServices.Create(newListDto);
            if (newList != null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);
        }

        private List<Guid> MovieToId(List<Movie> ListOfMovies)
        {
            var result = new List<Guid>();
            foreach (var movie in ListOfMovies)
            {
                result.Add(movie.ID);
            }
            return result;
        }
    }
}
