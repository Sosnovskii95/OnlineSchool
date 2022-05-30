using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client? Client { get; set; }

        public int CourseId { get; set; }

        public Course? Course { get; set; }

        public int AccessId { get; set; }

        public Access? Access { get; set; }
    }
}
