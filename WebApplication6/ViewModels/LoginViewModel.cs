using System.ComponentModel.DataAnnotations;

namespace WebApplication6.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Поле не заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }

}
