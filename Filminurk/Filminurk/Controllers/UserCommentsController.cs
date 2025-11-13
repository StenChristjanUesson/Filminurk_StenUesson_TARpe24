using System.ComponentModel.Design;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.UserComments;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IUserCommentsServices _userCommentsServices;
        public UserCommentsController(FilminurkTARpe24Context context, IUserCommentsServices userCommentsServices)
        {
            _context = context;
            _userCommentsServices = userCommentsServices;
        }

        public IActionResult Index()
        {
            var result = _context.UserComments
                .Select(c => new UserCommentIndexViewModel
                {
                    CommentID = c.CommentID,
                    CommenterBody = c.CommenterBody,
                    IsHarmful = (int)c.IsHarmful,
                    CommentCreatedAt = c.CommentCreatedAt,
                });
            return View(result);
        }
        [HttpGet]
        public IActionResult NewComment()
        {
            //TODO: erista kas tegemist on admini, või tavakasutajaga
            UserCommentCreateViewModel newcomment = new();
            return View(newcomment);
        }
        [HttpPost, ActionName("NewComment")]
        //meetodile ei tohi panna alllowAnonymous
        public async Task<IActionResult> NewCommentPost(UserCommentCreateViewModel newcommentVM)
        {
            // check dto
            //newcommentVM.CommenterUserID = "00000000-0000-0000-000000000001";//8-4-4-
            //TODO; newcommenti manuaalne sadmine, asenda pärast kasutaja id-ga
            Console.WriteLine(newcommentVM.CommenterUserID);
            if (ModelState.IsValid)
            {
                var dto = new UserCommentDto() { };
                dto.CommentID = newcommentVM.CommentID;
                dto.CommenterBody = newcommentVM.CommenterBody;
                dto.CommenterUserID = newcommentVM.CommenterUserID;
                dto.CommentedScore = newcommentVM.CommentedScore;
                dto.CommentCreatedAt = newcommentVM.CommentCreatedAt;
                dto.CommentModifiedAt = newcommentVM.CommentModifiedAt;
                dto.CommentDeletedAt = newcommentVM.CommentModifiedAt;
                dto.IsHelpful = newcommentVM.IsHelpful;
                dto.IsHarmful = newcommentVM.IsHarmful;

                var result = await _userCommentsServices.NewComment(dto);
                if (result == null)
                {
                    return NotFound();
                }
                //TODO: erista ära kas tegu admini või kasutajaga, admin tagastub admin-comments-index, kasutaja aga vastava flimi juurde
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Details", "Movies", id)
            }
            return NotFound();
        }
    }
}
