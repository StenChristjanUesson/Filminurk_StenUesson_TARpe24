using Filminurk.Core.Domain;

namespace Filminurk.Models.FavouritesLists
{
    public class FavoriteListUserDetailsViewModel
    {
        public Guid? FavouriteListID { get; set; }
        public string ListBelongsToUser { get; set; }
        public bool IsMovieOrActor { get; set; }
        public string ListName { get; set; }
        public string ListDescription { get; set; }
        public bool IsPrivate { get; set; }
        public List<Movie> ListOfMovies { get; set; }
        //public List<Actor>? ListOfActors { get; set; }

        /* andmebaasiomadused */

        public DateTime? ListCreatedAt { get; set; }
        public DateTime? ListUpdatedAt { get; set; }
        public DateTime? ListDeletedAt { get; set; }
        public bool? IsReported { get; set; }

        // imagemodel for index
        public List<FavouriteListIndexImageViewModel> Image { get; set; } = new List<FavouriteListIndexImageViewModel>();
    }
}
