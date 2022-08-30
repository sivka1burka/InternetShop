using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication6.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Display(Name = "Возраст")]
        [Range(21, 120, ErrorMessage = "Минимальный возраст - 21 год")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [StringLength(24, ErrorMessage = "Минимальная длина пароля - 8 символов, Максимальная длина - 24 символа", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не заполнено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }

}
