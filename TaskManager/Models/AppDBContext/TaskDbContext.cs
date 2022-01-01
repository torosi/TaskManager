using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.AppDBContext
{
    public class TaskDbContext : DbContext
    {

        public TaskDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Task> Tasks { get; set; }

    }
}
