using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название курса")]
        [Required(ErrorMessage = "Название курса")]
        public string TitleCourse { get; set; }

        [Display(Name = "Описание курса")]
        [MaxLength(5000, ErrorMessage = "Максимальная длинна 5000")]
        [Required(ErrorMessage = "Описание курса")]
        public string DescriptionCourse { get; set; }

        [Display(Name = "Стоимость")]
        [Range(typeof(decimal), "0,0", "100000,6", ErrorMessage = "Стоимость")]
        [Required(ErrorMessage = "Стоимость")]
        public decimal Price { get; set; }

        [Display(Name = "Изображение")]
        public int? ImageId { get; set; }

        [Display(Name = "Изображение")]
        public Image? Image { get; set; }

        public ICollection<Topic>? Topics { get; set; }
    }
}
