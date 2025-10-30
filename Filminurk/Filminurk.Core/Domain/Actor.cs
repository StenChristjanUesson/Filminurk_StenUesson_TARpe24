using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Domain
{
    public class Actor
    {
        public Guid ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? NickName { get; set; }
        public List<string> MoviesActedFor { get; set; }
        public int PortraitID { get; set; }

        /* 3 õpilase poolt väljamõeldud andmetüüpi millest üks peaks olema ENUM */

        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public FavoredGenreToActIn FavoredGenreToActIn { get; set; }
    }
}
