using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;

namespace Filminurk.Core.ServiceInterface
{
    public interface IFileServices // see in interface. asub .core/serviceinterface
    {
        void FilesToApi(MoviesDto dto, Movie domain);

        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);
        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dto);
    }
}
