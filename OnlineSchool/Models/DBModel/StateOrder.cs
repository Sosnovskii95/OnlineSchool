using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class StateOrder
    {
        [Key]
        public int Id { get; set; }

        public string TitleState { get; set; }
    }
}
