using System.ComponentModel.DataAnnotations;

namespace ProjectWired.Core.DTOs.User
{
    public class CreateUserDto
    {
        [Display(Name = "Şifre"), DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        
        [Display(Name = "E-mail"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
