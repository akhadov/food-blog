using FoodBlog.Domain.Common.Base;

namespace FoodBlog.Domain.TodoItems;

public record TodoItemCompletedEvent(TodoItem Item) : DomainEvent;
