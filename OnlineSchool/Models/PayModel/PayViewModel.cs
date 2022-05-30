using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.PayModel
{
    public class PayViewModel
    {
        [DataType(DataType.CreditCard)]
        [Display(Name = "Номер карты")]
        [Required(ErrorMessage = "Номер карты")]
        public string CardNumber { get; set; }

        [Display(Name = "Имя держателя")]
        [Required(ErrorMessage = "Имя держателя")]
        public string FullName { get; set; }

        [Display(Name = "Срок действия")]
        public int Moth { get; set; }

        public int Year { get; set; }

        [Display(Name = "Защитный код")]
        [Required(ErrorMessage = "Защитный код")]
        public int CVV { get; set; }
    }
}
