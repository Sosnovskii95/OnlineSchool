using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        public string TitleTopic { get; set; }

        public string DescriptionTopic { get; set; }

        public int CourseId { get; set; }

        public Course? Course { get; set; }
    }
}
