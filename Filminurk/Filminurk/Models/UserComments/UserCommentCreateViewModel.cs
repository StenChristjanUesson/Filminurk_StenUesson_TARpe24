using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.UserComments
{
    public class UserCommentCreateViewModel
    {
        public Guid? CommentID { get; set; }
        public string CommenterUserID { get; set; } = "00000000-0000-0000-000000000001"; //8-4-4-12
        public string CommenterBody { get; set; }
        public int CommentedScore { get; set; }
        public int? IsHelpful { get; set; } //👍 kasutaja ei saa loomise ajal muuta laike
        public int? IsHarmful { get; set; } //👎 ega dislaike

        /* Andmebaasi jaoks vajalikud andmed */
        public DateTime CommentCreatedAt { get; set; }
        public DateTime CommentModifiedAt { get; set; }
        public DateTime? CommentDeletedAt { get; set; } // kasutaja ei saa kustutada kommentaari loomise hetkel
    }
}
