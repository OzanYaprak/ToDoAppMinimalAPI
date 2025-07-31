using Microsoft.EntityFrameworkCore;
using ToDoAppMinimalAPI.Entities;

namespace ToDoAppMinimalAPI.Repositories
{
    public class ToDoAppDbContext : DbContext
    {
        public DbSet<Entities.Task> Tasks { get; set; }

        public ToDoAppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Task>().HasData(new Entities.Task
                {
                    Id = 1,
                    Title = "Sample Task",
                    Description = "This is a sample task description.",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(7),
                    CreatedBy = null,
                    LastModifiedBy = null,
                    IsDeleted = false
                }
            );
        }
    }
}
