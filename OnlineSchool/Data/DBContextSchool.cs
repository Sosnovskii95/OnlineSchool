using Microsoft.EntityFrameworkCore;
using OnlineSchool.Models.DBModel;

namespace OnlineSchool.Data
{
    public class DBContextSchool : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ResultTestLesson> ResultTestLessons { get; set; }

        public DbSet<TestLesson> TestLessons { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<User> Users { get; set; }

        public DBContextSchool(DbContextOptions<DBContextSchool> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
