using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.AccountModel
{
    public class RegisterModel
    {
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Электронная почта")]
        public string EmailClient { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль")]
        public string PasswordClient { get; set; }

        [Display(Name = "Повторите пароль")]
        [Compare("PasswordClient", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Повторите пароль")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "ФИО")]
        [Required(ErrorMessage = "ФИО")]
        public string FirstLastNameClient { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона")]
        public string NumberPhone { get; set; }
    }
}