using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;

namespace TodoListApi.Data
{
    public class TodoContext : DbContext
    {
        // Constructor to pass options (like database connection string) to the base DbContext
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        // DbSet to represent the Tasks table in the database
        public DbSet<TaskItem> Tasks { get; set; }

        // Seed initial data into the Tasks table when the database is created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Title = "Initial Task 1", Description = "Incomplete task 1", DueDate = new DateTime(2024, 12, 25), IsCompleted = false },
                new TaskItem { Id = 2, Title = "Initial Task 2", Description = "Complete task 2", DueDate = new DateTime(2024, 12, 25), IsCompleted = true }
            );
        }
    }
}
