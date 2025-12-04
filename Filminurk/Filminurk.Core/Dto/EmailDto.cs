using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.Core.Dto
{
    public class EmailDto
    {
        public string SendToThisAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailContent { get; set; }
    }
}
