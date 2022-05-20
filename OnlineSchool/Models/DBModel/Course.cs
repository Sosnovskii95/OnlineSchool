using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название курса")]
        public string TitleCourse { get; set; }

        [Display(Name = "Описание курса")]
        [MaxLength(5000)]
        public string DescriptionCourse { get; set; }

        [Display(Name = "Изображение")]
        public string? ImageFileName { get; set; }

        public string? ContentTypeFileName { get; set; }
    }
}
