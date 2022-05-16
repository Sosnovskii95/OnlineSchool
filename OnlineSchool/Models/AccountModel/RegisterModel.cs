using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.AccountModel
{
    public class RegisterModel
    {
        public string EmailClient { get; set; }

        public string LoginClient { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string PasswordClient { get; set; }

        [Display(Name = "Повторите пароль")]
        [Compare("PasswordClient", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Заполните данное поле")]
        public string PasswordConfirm { get; set; }

        public string FirstNameClient { get; set; }

        public string LastNameClient { get; set; }

        public int Age { get; set; }

        public string NumberPhone { get; set; }
    }
}
