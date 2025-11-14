using Filminurk.Data;
using Filminurk.Models.FavouritesLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouritesListController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        //fileservice add later
        //flservice add later
        public FavouritesListController( FilminurkTARpe24Context context )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.favouriteLists.OrderByDescending(y => y.ListCreatedAt) //sorteeri nimekiri langevas järjekorras kuupäeva-kellaaja järgi kuna me nimekirja tegime
            .Select(x => new FavouritesListsIndexViewModel
            {
                FavouriteListID = x.FavouriteListID,
                ListBelongsToUser = x.ListBelongsToUser,
                IsMovieOrActor = x.IsMovieOrActor,
                ListName = x.ListName,
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
            return View();
        }
    }
}
