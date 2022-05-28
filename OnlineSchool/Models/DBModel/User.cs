using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Электронная почта")]
        public string EmailUser { get; set; }

        [Display(Name = "Логин")]
        public string LoginUser { get; set; }

        [Display(Name = "Пароль")]
        public string PasswordUser { get; set; }

        [Display(Name = "Активность")]
        public bool ActiveUser { get; set; }

        [Display(Name = "Права доступа")]
        public int RoleId { get; set; }

        [Display(Name = "Права доступа")]
        public Role? Role { get; set; }
    }
}
