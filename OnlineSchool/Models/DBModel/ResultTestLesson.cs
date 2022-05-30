using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class ResultTestLesson
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Попытка")]
        public int HintTestLessonId { get; set; }

        [Display(Name = "Попытка")]
        public HintTestLesson? HintTestLesson { get; set; }

        [Display(Name = "Номер теста")]
        public int TestLessonId { get; set; }

        [Display(Name = "Номер теста")]
        public TestLesson? TestLesson { get; set; }

        [Display(Name = "Значение ответа")]
        public string ValueAnswer { get; set; }

        public bool ResultAnswer { get; set; }
    }
}
