using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Электронная почта")]
        [EmailAddress(ErrorMessage = "Некорректный электронный адрес")]
        public string EmailUser { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль")]
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }

        [Display(Name = "Фамилия Имя Отчество")]
        [Required(ErrorMessage = "Фамилия Имя Отчество")]
        public string FullNameUser { get; set; }

        [Display(Name = "Активность")]
        public bool ActiveUser { get; set; }

        [Display(Name = "Права доступа")]
        public int RoleId { get; set; }

        [Display(Name = "Права доступа")]
        public Role? Role { get; set; }
    }
}
