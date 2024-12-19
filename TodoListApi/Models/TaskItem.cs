using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models
{
    // Represents a task in the TODO list
    public class TaskItem
    {
        // Primary key for the Tasks table
        public int Id { get; set; }

        // Title of the task, required and with a maximum length of 100 characters
        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        // Description of the task, optional and with a maximum length of 500 characters
        [MaxLength(500)]
        public string? Description { get; set; }

        // Due date of the task, sets to current date if missing
        public DateTime? DueDate { get; set; } = DateTime.Now;

        // Indicates whether the task is completed, defaults to false
        public bool IsCompleted { get; set; } = false;
    }
}
