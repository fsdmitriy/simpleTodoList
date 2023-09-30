using Microsoft.AspNetCore.Mvc;
using SimpleTODOList.Model;
using SimpleTODOList.Repositories;

namespace SimpleTODOList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _repository;

    public TodoController(ITodoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodoItems()
    {
        var todos = await _repository.GetTodosAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodoItem(int id)
    {
        Todo todo;
        try
        {
            todo = await _repository.GetTodoAsync(id);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch
        {
            return BadRequest();
        }

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodoItem(Todo todoItem)
    {
        var todo = await _repository.CreateTodoAsync(todoItem);
        return CreatedAtAction(nameof(GetTodoItem), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(int id, Todo todoItem)
    {
        if (id != todoItem.Id)
        {
            return NotFound();
        }

        await _repository.UpdateTodoAsync(todoItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        try
        {
            await _repository.DeleteTodoAsync(id);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch
        {
            return BadRequest();
        }
        return NoContent();
    }
}
