using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Data;
using TodoListApi.Models;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TodoContext _context;

        public TasksController(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of tasks.
        /// </summary>
        /// <remarks>
        /// You can filter tasks by their completion status and sort them by due date.
        /// </remarks>
        /// <param name="isCompleted">Optional filter to retrieve only completed or incomplete tasks.</param>
        /// <param name="sortDescending">Optional boolean to sort by due date in descending order. Defaults to false (ascending).</param>
        /// <returns>A list of tasks.</returns>
        [HttpGet]
        [ProducesResponseType(200)]  // 200 OK for successful retrieval
        [ProducesResponseType(400)]  // 400 Bad Request for invalid parameters
        public async Task<IActionResult> GetTasks([FromQuery] bool? isCompleted, [FromQuery] bool sortDescending = false)
        {
            var query = _context.Tasks.AsQueryable();

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            // Use the boolean parameter for sorting
            query = sortDescending ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate);

            return Ok(await query.ToListAsync());
        }

        /// <summary>
        /// Retrieves a specific task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>The requested task or a 404 Not Found response.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]  // 200 OK for successful retrieval
        [ProducesResponseType(404)]  // 404 Not Found if the task doesn't exist
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">The task details to create.</param>
        /// <returns>The created task with its ID.</returns>
        [HttpPost]
        [ProducesResponseType(201)]  // 201 Created when a new task is created
        [ProducesResponseType(400)]  // 400 Bad Request for invalid data
        public async Task<IActionResult> CreateTask(TaskItem task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="id">The ID of the task to update.</param>
        /// <param name="task">The updated task details.</param>
        /// <returns>A 204 No Content response on success or a 404 Not Found response if the task does not exist.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]  // 204 No Content if the task is updated successfully
        [ProducesResponseType(400)]  // 400 Bad Request for invalid data or mismatched ID
        [ProducesResponseType(404)]  // 404 Not Found if the task doesn't exist
        public async Task<IActionResult> UpdateTask(int id, TaskItem task)
        {
            if (id != task.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>A 204 No Content response on success or a 404 Not Found response if the task does not exist.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]  // 204 No Content if the task is deleted successfully
        [ProducesResponseType(404)]  // 404 Not Found if the task doesn't exist
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a task exists by ID
        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}