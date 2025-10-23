using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IMovieServices // see in interface. asub .core/serviceinterface
    {
        Task<Movie> Create (MoviesDto dto);
        Task<Movie> DetailsAsync (Guid id);
        Task<Movie> Update (MoviesDto dto);
        Task<Movie> Delete (Guid id);
    }
}
