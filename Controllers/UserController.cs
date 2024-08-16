using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Todo_A_P_I.Datas;
using Todo_A_P_I.Models;

namespace Todo_A_P_I.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TodoContext _context;

        public UserController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }
        [HttpPost("add-newTodoItem")]
        public async Task<IActionResult> AddNewTodoItem([FromBody] TodoItemDto todoItemDto)
        {
            // Get the current user's ID from the JWT token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            // Create a new TodoItem
            var todoItem = new TodoItem
            {
                Title = todoItemDto.Title,
                Description = todoItemDto.Description,
                IsCompleted = todoItemDto.IsCompleted,
                UserId = userId  // Assign the authenticated user's ID
            };

            // Add the todo item to the database
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return Ok(todoItem);
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null) return NotFound();

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

