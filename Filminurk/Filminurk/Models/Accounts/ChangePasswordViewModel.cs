using System.ComponentModel.DataAnnotations;

namespace Filminurk.Models.Accounts
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sisesta oma pragune parool: ")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Sisesta oma uus parool")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "kirjuta oma uus parool uuesti")]
        [Compare("NewPassword", ErrorMessage = "paroolid ei kattu, palun proovi uuesti.")]
        public string ConfirmNewPassword { get; set; }
    }
}
