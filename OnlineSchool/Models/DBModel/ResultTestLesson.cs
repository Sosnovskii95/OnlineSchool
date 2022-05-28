using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class ResultTestLesson
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Клиент")]
        public int? ClientId { get; set; }

        [Display(Name = "Клиент")]
        public Client? Client { get; set; }

        [Display(Name = "Номер теста")]
        public int TestLessonId { get; set; }

        [Display(Name = "Номер теста")]
        public TestLesson? TestLesson { get; set; }

        [Display(Name = "Значение ответа")]
        public string ValueAnswer { get; set; }
    }
}
