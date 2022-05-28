using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Изображение")]
        public string FileName { get; set; }

        [Display(Name = "Тип изображения")]
        public string ContentType { get; set; }
    }
}
