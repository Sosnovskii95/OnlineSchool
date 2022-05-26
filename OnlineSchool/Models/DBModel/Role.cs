using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Права доступа")]
        public string TitleRole { get; set; }

        [Display(Name = "Значение доступа")]
        public string ValueRole { get; set; }
    }
}
