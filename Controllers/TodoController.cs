using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Todo_A_P_I.Datas;

namespace Todo_A_P_I.Controllers
{
    [Route("Todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        // DELETE: /Todo/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null) return NotFound();

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchTodoItems([FromQuery] string term)
        {
            // Check if the search term is provided
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("Search term cannot be empty.");
            }

            // Get the current user's ID from the JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Search TodoItems where Title or Description contains the search term
            var todoItems = await _context.TodoItems
                .Where(t => t.UserId == userId && (t.Title.Contains(term) || t.Description.Contains(term)))
                .ToListAsync();

            // Return the found items
            if (todoItems == null || todoItems.Count == 0)
            {
                return NotFound("No todo items found matching the search term.");
            }

            return Ok(todoItems);
        }

    }


}
