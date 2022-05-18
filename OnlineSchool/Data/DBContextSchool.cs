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

        public DbSet<StateOrder> StateOrders { get; set; }

        public DbSet<PayMethod> PayMethods { get; set; }

        public DbSet<Access> Accesses { get; set; }

        public DBContextSchool(DbContextOptions<DBContextSchool> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StateOrder>().HasData(new List<StateOrder> { new StateOrder { Id = 1, TitleState = "В обработке" }, new StateOrder { Id = 2, TitleState = "Одобрен" }, new StateOrder { Id = 3, TitleState = "Завершен" } });
            modelBuilder.Entity<PayMethod>().HasData(new List<PayMethod> { new PayMethod { Id = 1, TitlePayMethod = "Наличные" }, new PayMethod { Id = 2, TitlePayMethod = "Карта" } });
            modelBuilder.Entity<Access>().HasData(new List<Access> { new Access { Id = 1, TitleAccess = "Разрешен", ValueAccess = true }, new Access { Id = 2, TitleAccess = "Запрещен", ValueAccess = false } });
        }
    }
}
