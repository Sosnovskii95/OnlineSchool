using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Клиент")]
        public int ClientId { get; set; }

        [Display(Name = "Клиент")]
        public Client? Client { get; set; }

        [Display(Name = "Курс")]
        public int CourseId { get; set; }

        [Display(Name = "Курс")]
        public Course? Course { get; set; }

        [Display(Name = "Пользователь")]
        public int? UserId { get; set; }

        [Display(Name = "Пользователь")]
        public User? User { get; set; }

        [Display(Name = "Доступ")]
        public int AccessId { get; set; }

        [Display(Name = "Доступ")]
        public Access? Access { get; set; }
    }
}