using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }
    }
}
