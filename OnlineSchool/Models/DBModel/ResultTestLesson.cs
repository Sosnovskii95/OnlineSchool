using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class ResultTestLesson
    {
        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int TestLessonId { get; set; }

        public TestLesson TestLesson { get; set; }

        public string ValueAnswer { get; set; }
    }
}
