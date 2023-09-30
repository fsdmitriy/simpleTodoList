using SimpleTODOList.Model;
using SimpleTODOList.Repositories;

namespace SimpleTODOList.Test;

public class TodoRepositoryMock : ITodoRepository
{
    private List<Todo> _todos;

    public TodoRepositoryMock()
    {
        _todos = new List<Todo>
        {
                new Todo { Id = 1, Title = "Todo Item 1", IsCompleted = false },
                new Todo { Id = 2, Title = "Todo Item 2", IsCompleted = true },
                new Todo { Id = 3, Title = "Todo Item 3", IsCompleted = false }
        };
    }

    public Task<IEnumerable<Todo>> GetTodosAsync()
    {
        return Task.FromResult<IEnumerable<Todo>>(_todos);
    }

    public Task<Todo> GetTodoAsync(int id)
    {
        var todo = _todos.SingleOrDefault(t => t.Id == id);
        if (todo == null)
        {
            throw new ArgumentException($"Todo with ID {id} not found.");
        }
        return Task.FromResult(todo);
    }

    public async Task<Todo> CreateTodoAsync(Todo todo)
    {
        todo.Id = _todos.Count + 1;
        _todos.Add(todo);
        return await GetTodoAsync(todo.Id);
    }

    public async Task UpdateTodoAsync(Todo todo)
    {
        var index = _todos.FindIndex(t => t.Id == todo.Id);
        if (index != -1)
        {
            _todos[index] = todo;
        }
        else
        {
            throw new ArgumentException($"Todo with ID {todo.Id} not found.");
        }
        await Task.CompletedTask;
    }

    public async Task DeleteTodoAsync(int id)
    {
        var index = _todos.FindIndex(t => t.Id == id);
        if (index != -1)
        {
            _todos.RemoveAt(index);
        }
        else
        {
            throw new ArgumentException($"Todo with ID {id} not found.");
        }

        await Task.CompletedTask;
    }
}
