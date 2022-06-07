using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название урока")]
        [Required(ErrorMessage = "Название урока")]
        public string TitleLesson { get; set; }

        [Display(Name = "Описание урока")]
        [MaxLength(5000, ErrorMessage = "Максимальная длина 5000")]
        [Required(ErrorMessage = "Описание урока")]
        public string DescriptionLesson { get; set; }

        [Display(Name = "Ссылка на видео (youtube)")]
        [DataType(DataType.Url)]
        [Required(ErrorMessage = "Ссылка на видео (youtube)")]
        public string URLVideo { get; set; }

        [Display(Name = "Тема")]
        public int TopicId { get; set; }

        [Display(Name = "Тема")]
        public Topic? Topic { get; set; }

        [Display(Name = "Тесты урока")]
        public ICollection<TestLesson>? TestLessons { get; set; }
    }
}
