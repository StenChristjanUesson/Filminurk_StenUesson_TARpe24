namespace Filminurk.Models.Movies
{
    public class MoviesCreateUpdateViewModel
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly FirstPublished { get; set; }
        public string Director { get; set; }
        public List<string>? Actors { get; set; }
        public decimal? CurrentRatting { get; set; }
        public DateTime? EntryCreatedAt { get; set; }
        public DateTime? EntryModifiedAt { get; set; }
        //public List<UserComment>? reviews { get; set; }

        /* 3 õpilase valitud andmetüüpi */

        public decimal? MovieCreationCost { get; set; }
        public List<string>? Studio { get; set; }
        public Genre genre { get; set; }
    }
}
