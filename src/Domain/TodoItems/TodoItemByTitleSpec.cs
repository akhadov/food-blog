using Ardalis.Specification;

namespace FoodBlog.Domain.TodoItems;

public sealed class TodoItemByTitleSpec : Specification<TodoItem>
{
    public TodoItemByTitleSpec(string title)
    {
        Query.Where(i => i.Title == title);
    }
}
