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

        public DbSet<Access> Accesses { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<HintTestLesson> HintTestLessons { get; set; }

        public DBContextSchool(DbContextOptions<DBContextSchool> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Access>().HasData(new List<Access>
            {
                new Access { Id = 1, TitleAccess = "Разрешен", ValueAccess = true },
                new Access { Id = 2, TitleAccess = "Запрещен", ValueAccess = false }
            });
            modelBuilder.Entity<Role>().HasData(new List<Role>
            {
                new Role {Id = 1, TitleRole="Администратор", ValueRole="admin"},
                new Role {Id=2, TitleRole="Учитель", ValueRole="teacher"},
                new Role{Id=3, TitleRole="Методист", ValueRole="metodist"}
            });
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User {Id = 1, EmailUser = "admin@admin", FullNameUser = "Администратор",  PasswordUser = "admin", RoleId = 1, ActiveUser = true},
                new User{Id=2, EmailUser ="teacher@teacher", FullNameUser = "Учитель", PasswordUser = "teacher", RoleId = 2, ActiveUser = true},
                new User {Id = 3, EmailUser ="metodist@metodist", FullNameUser = "Методист", PasswordUser = "metodist", RoleId =3, ActiveUser = true}
            });
            modelBuilder.Entity<Client>().HasData(new Client
            {
                Id = 1,
                EmailClient = "test@test",
                FirstLastNameClient = "test",
                PasswordClient = "test",
                Age = 25,
                NumberPhone = "37529333333"
            });
        }
    }
}
