using Microsoft.EntityFrameworkCore;

namespace DB_Task.Models
{
    public class ApplicationContext : DbContext
    {
        private readonly string connectionString = "server=localhost;port=3306;uid=ur_user;password=ur_password;database=tovarsDb;";
        public DbSet<TovarsModel> Tovars { get; set; }
        public DbSet<CategoryModel> Category { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, 
                ServerVersion.AutoDetect(connectionString));
        }


    }
}
