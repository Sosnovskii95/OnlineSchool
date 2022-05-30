using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Access
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Доступ")]
        public string TitleAccess { get; set; }

        [Display(Name = "Значение доступа")]
        public bool ValueAccess { get; set; }
    }
}
