using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models.AppDBContext
{
    public class TaskDbContext : DbContext
    {

        private readonly DbContextOptions _options;

        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            _options = options;
        }   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }

    }
}
