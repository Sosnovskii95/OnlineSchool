using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string EmailClient { get; set; }

        public string LoginClient { get; set; }

        public string PasswordClient { get; set; }

        public string FirstNameClient { get; set; }

        public string LastNameClient { get; set; }

        public int Age { get; set; }

        public string NumberPhone { get; set; }
    }
}
