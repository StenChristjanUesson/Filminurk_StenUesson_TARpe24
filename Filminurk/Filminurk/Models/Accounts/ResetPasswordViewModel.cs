using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Accounts
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "kirjuta oma uus parool uuesti")]
        [Compare("Password", ErrorMessage = "paroolid ei kattu, palun proovi uuesti.")]
        public string ConfirmNewPassword { get; set; }
        public string Token { get; set; }
    }
}
