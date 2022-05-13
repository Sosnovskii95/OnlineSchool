using System.ComponentModel.DataAnnotations;

namespace OnlineSchool.Models.DBModel
{
    public class TestLesson
    {
        [Key]
        public int Id { get; set; }

        public int LessonId { get; set; }

        public string Question { get; set; }

        public string AnswerOne { get; set; }

        public string AnswerTwo { get; set; }

        public string AnswerThree { get; set; }

        public string AnswerFour { get; set; }

        public string AnswerFive { get; set; }

        public string RightAnswer { get; set; }
    }
}
