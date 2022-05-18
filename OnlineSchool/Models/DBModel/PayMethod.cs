using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class PayMethod
    {
        [Key]
        public int Id { get; set; }

        public string TitlePayMethod { get; set; }
    }
}
