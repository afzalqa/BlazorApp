using BlazorApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodos), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut]
        public async Task<IActionResult> EditTodo(TodoItem todoItem)
        {
            var existingTodo = await _context.TodoItems.FindAsync(todoItem.Id);
            if (existingTodo == null)
            {
                return BadRequest(new { code = 2, message = "Todo item not found." }); 
            }
            
            existingTodo.Title = todoItem.Title;
            existingTodo.IsCompleted = todoItem.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return BadRequest(new { code = 1, message = "Todo item not found." });
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
