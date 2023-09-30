using Microsoft.AspNetCore.Mvc;
using SimpleTODOList.Controllers;
using SimpleTODOList.Model;
using SimpleTODOList.Repositories;

namespace SimpleTODOList.Test;

[TestFixture]
public class TodoControllerTests
{
    private ITodoRepository _repository;

    [SetUp]
    public void Setup()
    {
        _repository = new TodoRepositoryMock();
    }

    [Test]
    public async Task Test_GetTodoItems_Returns_ValidResult()
    {
        // Arrange
        var controller = new TodoController(_repository);

        // Act
        var result = await controller.GetTodoItems();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var value = (result.Result as OkObjectResult)?.Value;
        Assert.IsInstanceOf<IEnumerable<Todo>>(value);
    }

    [Test]
    public async Task Test_GetTodo_Returns_ValidResult()
    {
        // Arrange
        var controller = new TodoController(_repository);
        var id = 1;

        // Act
        var result = await controller.GetTodoItem(id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var value = (result.Result as OkObjectResult)?.Value;
        Assert.IsInstanceOf<Todo>(value);
        Assert.That((value as Todo)?.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task Test_GetTodo_Returns_NotFound_For_Invalid_Id()
    {
        // Arrange
        var controller = new TodoController(_repository);
        var id = 5;

        // Act
        var result = await controller.GetTodoItem(id);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Test_CreateTodo_Returns_ValidResult()
    {
        // Arrange
        var controller = new TodoController(_repository);
        var todo = new Todo { Title = "New Todo Item", IsCompleted = false };

        // Act
        var result = await controller.CreateTodoItem(todo);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var value = (result.Result as CreatedAtActionResult)?.Value;
        Assert.IsInstanceOf<Todo>(value);
        Assert.That((value as Todo)?.Title, Is.EqualTo(todo.Title));
        Assert.That((value as Todo)?.IsCompleted, Is.EqualTo(todo.IsCompleted));
    }

    [Test]
    public async Task Test_UpdateTodo_Returns_ValidResult()
    {
        // Arrange
        var controller = new TodoController(_repository);
        var todo = await _repository.GetTodoAsync(1);
        todo.Title = "Updated Todo Item";

        // Act
        var result = await controller.UpdateTodoItem(todo.Id, todo);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task Test_UpdateTodo_Returns_NotFound_For_Invalid_Id()
    {
        // Arrange
        var controller = new TodoController(_repository);
        var todo = await _repository.GetTodoAsync(1);
        var id = 5;

        // Act
        var result = await controller.UpdateTodoItem(id, todo);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Test_DeleteTodo_Returns_ValidResult()
    {
        // Arrange
        var controller = new TodoController(_repository);
        var id = 1;

        // Act
        var result = await controller.DeleteTodoItem(id);

        // Assert
        Assert.IsInstanceOf<NoContentResult>(result);
    }

    [Test]
    public async Task Test_DeleteTodo_Returns_NotFound_For_Invalid_Id()
    {
        // Arrange
        var controller = new TodoController(_repository);
        var id = 5;

        // Act
        var result = await controller.DeleteTodoItem(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
}