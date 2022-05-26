using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string EmailUser { get; set; }

        public string LoginUser { get; set; }

        public string PasswordUser { get; set; }

        public bool ActiveUser { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
