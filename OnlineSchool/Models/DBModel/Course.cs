using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string TitleCourse { get; set; }

        public string DescriptionCourse { get; set; }
    }
}
