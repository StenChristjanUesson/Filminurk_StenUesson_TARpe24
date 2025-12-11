using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto.AccountsDtos
{
    public class ApplicationUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool ProfileType { get; set; }
        public List<Guid>? favouriteListIds { get; set; }
        public List<Guid>? CommentIds { get; set; }
        public string AvatarImageId { get; set; }
        public string DisplayName { get; set; }

        /* 2 minu personal väljamõeldud andmevälja */

        public string Mood { get; set; }
        public string Description { get; set; }
    }
}
