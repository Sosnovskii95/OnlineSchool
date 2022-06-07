using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название темы")]
        [Required(ErrorMessage = "Название темы")]
        public string TitleTopic { get; set; }

        [Display(Name = "Описание темы")]
        [MaxLength(5000, ErrorMessage = "Максимальное количество символов 5000")]
        [Required(ErrorMessage = "Описание темы")]
        public string DescriptionTopic { get; set; }

        [Display(Name = "Курс")]
        public int? CourseId { get; set; }

        [Display(Name = "Курс")]
        public Course? Course { get; set; }

        [Display(Name = "Изображение")]
        public int? ImageId { get; set; }

        [Display(Name = "Изображение")]
        public Image? Image { get; set; }

        public ICollection<Lesson>? Lessons { get; set; }
    }
}
