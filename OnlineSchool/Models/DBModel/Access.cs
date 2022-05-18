using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Access
    {
        [Key]
        public int Id { get; set; }

        public string TitleAccess { get; set; }

        public bool ValueAccess { get; set; }
    }
}
