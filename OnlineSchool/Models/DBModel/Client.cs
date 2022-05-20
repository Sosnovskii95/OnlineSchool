using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Электронная почта")]
        public string EmailClient { get; set; }

        [Display(Name = "Логин клиента")]
        public string LoginClient { get; set; }

        [Display(Name = "Пароль")]
        public string PasswordClient { get; set; }

        [Display(Name = "Фамилия Имя Отчества")]
        public string FirstLastNameClient { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Номер телефона")]
        public string NumberPhone { get; set; }
    }
}
