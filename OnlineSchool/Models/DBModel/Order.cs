using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int CourceId { get; set; }

        public Course Course { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string StateOrder { get; set; }
    }
}
