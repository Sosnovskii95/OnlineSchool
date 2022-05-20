using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Access
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "")]
        public string TitleAccess { get; set; }

        [Display(Name = "")]
        public bool ValueAccess { get; set; }
    }
}
