using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.ViewModels.AccountVM
{
    public class SignUpPageVM
    {
        [EmailAddress]
        [DisplayName("E-posta")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakterden oluşmalıdır")]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakterden oluşmalıdır")]
        [DisplayName("Şifreyi tekrarla")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmamaktadır")]
        public string ConfirmationPassword { get; set; }
    }
}
