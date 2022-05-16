using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.AccountModel
{
    public class LoginModel
    {
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Вход для администратора")]
        public bool InvateAdmin { get; set; }
    }
}
