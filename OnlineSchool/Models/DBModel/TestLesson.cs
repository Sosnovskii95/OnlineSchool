using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class TestLesson
    {
        [Key]
        [Display(Name = "Номер вопроса")]
        public int Id { get; set; }

        [Display(Name = "Урок")]
        public int LessonId { get; set; }

        [Display(Name = "Урок")]
        public Lesson? Lesson { get; set; }

        [Display(Name = "Вопрос")]
        [Required(ErrorMessage = "Вопрос")]
        public string Question { get; set; }

        [Display(Name = "Ответ 1")]
        public string? AnswerOne { get; set; }

        [Display(Name = "Ответ 2")]
        public string? AnswerTwo { get; set; }

        [Display(Name = "Ответ 3")]
        public string? AnswerThree { get; set; }

        [Display(Name = "Ответ 4")]
        public string? AnswerFour { get; set; }

        [Display(Name = "Ответ 5")]
        public string? AnswerFive { get; set; }

        [Display(Name = "Правильный ответ. Укажите номер ответа, а не его значение")]
        [Required(ErrorMessage = "Правильный ответ")]
        [Range(1, 5, ErrorMessage = "Значение от 1 до 5")]
        public string RightAnswer { get; set; }

        [Display(Name = "Подсказка")]
        [Required(ErrorMessage = "Подсказка")]
        public string HintQuestion { get; set; }
    }
}