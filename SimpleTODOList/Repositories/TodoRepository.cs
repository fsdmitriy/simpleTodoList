using Microsoft.EntityFrameworkCore;
using SimpleTODOList.Data;
using SimpleTODOList.Model;

namespace SimpleTODOList.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Todo>> GetTodosAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todo> GetTodoAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        return todo ?? throw new ArgumentException($"Todo with ID {id} not found.");
    }

    public async Task<Todo> CreateTodoAsync(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return todo;
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        _context.Entry(todo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoAsync(int id)
    {
        var todo = await GetTodoAsync(id);
        if (todo != null)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }
}
