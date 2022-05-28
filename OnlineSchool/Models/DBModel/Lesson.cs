using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название урока")]
        public string TitleLesson { get; set; }

        [Display(Name = "Описание урока")]
        [MaxLength(5000)]
        public string DescriptionLesson { get; set; }

        [Display(Name = "Ссылка на видео (youtube)")]
        [DataType(DataType.Url)]
        public string URLVideo { get; set; }

        [Display(Name = "Тема")]
        public int TopicId { get; set; }

        [Display(Name = "Тема")]
        public Topic? Topic { get; set; }

        [Display(Name = "Тесты урока")]
        public ICollection<TestLesson>? TestLessons { get; set; }
    }
}
