using FoodBlog.Domain.Common.Base;

namespace FoodBlog.Domain.TodoItems;

public record TodoItemCreatedEvent(TodoItem Item) : DomainEvent;
