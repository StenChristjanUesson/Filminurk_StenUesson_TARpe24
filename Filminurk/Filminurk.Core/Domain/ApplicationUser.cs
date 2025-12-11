using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Filminurk.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public List<Guid>? favouriteListIds { get; set; }
        public List<Guid>? CommentIds { get; set; }
        public string AvatarImageId { get; set; }
        public string DisplayName { get; set; }
        public bool ProfileType { get; set; }

        /* 2 minu personal väljamõeldud andmevälja */

        public string Mood { get; set; }
        public string Description { get; set; }
    }
}
