using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFileServices _fileServices; //failid

        public MovieServices( FilminurkTARpe24Context context, IFileServices fileServices )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Movie> Create(MoviesDto dto)
        {
            Movie movie = new Movie();
            movie.ID = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Director = dto.Director;
            movie.Actors = dto.Actors;
            movie.CurrentRatting = dto.CurrentRatting;
            movie.MovieCreationCost = dto.MovieCreationCost; //mine
            movie.Studio = dto.Studio; //mine
            movie.genre = (Genre)dto.genre; //mine
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }
        public async Task<Movie> DetailsAsync(Guid id)
        {
            var result = await _context.Movies.FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<Movie> Update(MoviesDto dto)
        {
            Movie movie = new Movie();
            movie.ID = Guid.NewGuid();
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.FirstPublished = (DateOnly)dto.FirstPublished;
            movie.Director = dto.Director;
            movie.Actors = dto.Actors;
            movie.CurrentRatting = dto.CurrentRatting;
            movie.MovieCreationCost = dto.MovieCreationCost; //mine
            movie.Studio = dto.Studio; //mine
            movie.genre = (Genre)dto.genre; //mine
            movie.EntryCreatedAt = DateTime.Now;
            movie.EntryModifiedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, movie);

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie> Delete(Guid id)
        {

            var result = await _context.Movies
                .FirstOrDefaultAsync(m => m.ID == id);

            var images = await _context.FilesToApi.Where(x => x.MovieID == id).Select(y => new FileToApiDto
            {
                ImageID = y.ImageID,
                MovieID = y.MovieID,
                FilePath = y.ExistingFilePath,
            }).ToArrayAsync();

            await _fileServices.RemoveImagesFromApi(images);
            _context.Movies.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}