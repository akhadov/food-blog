using Microsoft.EntityFrameworkCore;
using FoodBlog.Application.Common.Exceptions;
using FoodBlog.Application.Common.Interfaces;
using FoodBlog.Domain.TodoItems;

namespace FoodBlog.Application.Features.TodoItems.Commands.CompleteTodoItem;

public record CompleteTodoItemCommand(Guid TodoItemId) : IRequest;

// ReSharper disable once UnusedType.Global
public class CompleteTodoItemCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CompleteTodoItemCommand>
{
    public async Task Handle(CompleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItemId = new TodoItemId(request.TodoItemId);

        var todoItem = await dbContext.TodoItems
            .WithSpecification(new TodoItemByIdSpec(todoItemId))
            .FirstOrDefaultAsync(cancellationToken);

        if (todoItem is null)
            throw new NotFoundException(nameof(TodoItem), todoItemId);

        todoItem.Complete();

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}