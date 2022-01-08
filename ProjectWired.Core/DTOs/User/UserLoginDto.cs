using System.ComponentModel.DataAnnotations;

namespace ProjectWired.Core.DTOs.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email alanı zorunludur"), DataType(DataType.EmailAddress, ErrorMessage = "Doğru email formatında giriş yapınız!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur"), DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
