using System.ComponentModel.DataAnnotations;

namespace kayit.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Şifreler uyumsuz")]
        public string ConfirmPassword { get; set; }
    }
}
