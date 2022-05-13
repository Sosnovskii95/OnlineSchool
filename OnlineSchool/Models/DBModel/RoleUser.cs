using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class RoleUser
    {
        [Key]
        public int Id { get; set; }

        public string TitleRole { get; set; }

        public int ValueRole { get; set; }
    }
}
