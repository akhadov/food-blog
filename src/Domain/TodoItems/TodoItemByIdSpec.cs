using Ardalis.Specification;

namespace FoodBlog.Domain.TodoItems;

public sealed class TodoItemByIdSpec : SingleResultSpecification<TodoItem>
{
    public TodoItemByIdSpec(TodoItemId todoItemId)
    {
        Query.Where(t => t.Id == todoItemId);
    }
}