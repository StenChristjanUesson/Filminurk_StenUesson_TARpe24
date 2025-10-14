using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;

namespace Filminurk.Core.Dto.ServiceInterface
{
    public interface IMovieServices // see in interface. asub .core/serviceinterface
    {
        Task<Movie> Create (MoviesDto dto);
        Task<Movie> DetailsAsync (Guid id);
    }
}
