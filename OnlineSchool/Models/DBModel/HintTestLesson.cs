using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class HintTestLesson
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client? Client { get; set; }

        public int NumberHint { get; set; }

        public int CountRigth { get; set; }

        public double? ValueResult { get; set; }
    }
}
