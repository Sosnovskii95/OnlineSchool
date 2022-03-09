using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        public string TitleLesson { get; set; }

        public string DescriptionLesson { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }

        public ICollection<TestLesson> TestLessons { get; set; }
    }
}
