using SimpleTODOList.Model;

namespace SimpleTODOList.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetTodosAsync();
    Task<Todo> GetTodoAsync(int id);
    Task<Todo> CreateTodoAsync(Todo todo);
    Task UpdateTodoAsync(Todo todo);
    Task DeleteTodoAsync(int id);
}
