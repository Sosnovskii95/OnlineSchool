using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Электронная почта")]
        [EmailAddress(ErrorMessage = "Некорректная электронная почта")]
        [Required(ErrorMessage = "Электронная почта")]
        public string EmailClient { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль")]
        [DataType(DataType.Password)]
        public string PasswordClient { get; set; }

        [Display(Name = "Фамилия Имя Отчества")]
        [Required(ErrorMessage = "Фамилия Имя Отчества")]
        public string FirstLastNameClient { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Номер телефона")]
        public string NumberPhone { get; set; }
    }
}