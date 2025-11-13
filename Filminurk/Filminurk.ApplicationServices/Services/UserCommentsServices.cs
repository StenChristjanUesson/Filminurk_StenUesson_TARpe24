using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;

namespace Filminurk.ApplicationServices.Services
{
    public class UserCommentsServices : IUserCommentsServices
    {
        private readonly FilminurkTARpe24Context _context;

        public UserCommentsServices( FilminurkTARpe24Context context)
        {
            _context = context;
        }

        public async Task<UserComment> NewComment(UserCommentDto newcommentDto)
        {
            UserComment domain = new UserComment();

            domain.CommentID = Guid.NewGuid();
            domain.CommenterBody = newcommentDto.CommenterBody;
            domain.CommenterUserID = newcommentDto.CommenterUserID;
            domain.CommentedScore = newcommentDto.CommentedScore;
            domain.CommentCreatedAt = DateTime.Now;
            domain.CommentModifiedAt = DateTime.Now;
            domain.IsHelpful = 0;
            domain.IsHarmful = 0;

            await _context.UserComments.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
