using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class PayMethod
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Способ оплаты")]
        public string TitlePayMethod { get; set; }
    }
}
