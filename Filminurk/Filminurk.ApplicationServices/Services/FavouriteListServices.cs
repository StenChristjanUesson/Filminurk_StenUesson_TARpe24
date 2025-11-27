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
    public class FavouriteListsServices : IFavouriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFileServices _filesServices;

        public FavouriteListsServices(FilminurkTARpe24Context context, IFileServices filesServices)
        {
            _context = context;
            _filesServices = filesServices;
        }
        public async Task<favouriteList> DetailsAsync(Guid id)
        {
            var result = await _context.favouriteLists.FirstOrDefaultAsync(x => x.FavouriteListID == id);
            return result;
        }
        public async Task<favouriteList> Create(FavouriteListDto dto/*, List<Movie> selectedMovies*/)
        {
            favouriteList newList = new();
            newList.FavouriteListID = dto.FavouriteListID;
            newList.ListName = dto.ListName;
            newList.ListDescription = dto.ListDescription;
            newList.IsPrivate = dto.IsPrivate;
            newList.ListCreatedAt = dto.ListCreatedAt;
            newList.ListDeletedAt = dto.ListDeletedAt;
            newList.ListModifiedAt = dto.ListModifiedAt;
            newList.ListOfMovies = dto.ListOfMovies;
            await _context.favouriteLists.AddAsync(newList);
            await _context.SaveChangesAsync();
            /*foreach (var movie in selectedMovies)
            {
                _context.Entry(movie).Property(p => p.ID);
            } */
            return newList;
        }
        public async Task<favouriteList> Update(FavouriteListDto updatedList)
        {

        }
    }
}
