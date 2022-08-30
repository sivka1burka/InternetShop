using System.ComponentModel.DataAnnotations;

namespace WebApplication6.ViewModels
{
    public class ChangeUserViewModel
    {
        
        public string Name { get; set; }

        
        public string Surname { get; set; }

        
        public string Phone { get; set; }

        
        [Range(21, 120, ErrorMessage = "Минимальный возраст - 21 год")]
        public int Age { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        [StringLength(24, ErrorMessage = "Минимальная длина пароля - 8 символов, Максимальная длина - 24 символа", MinimumLength = 8)]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        
        public string PasswordConfirm { get; set; }
    }
}
