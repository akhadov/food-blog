using Microsoft.Extensions.Logging;
using FoodBlog.Domain.TodoItems;

namespace FoodBlog.Application.Features.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    : INotificationHandler<TodoItemCreatedEvent>
{
    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("TodoItemCreatedEventHandler: {Title} was created", notification.Item.Title);

        return Task.CompletedTask;
    }
}