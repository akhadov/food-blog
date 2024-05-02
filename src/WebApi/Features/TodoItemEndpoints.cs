using MediatR;
using FoodBlog.Application.Features.TodoItems.Commands.CreateTodoItem;
using FoodBlog.Application.Features.TodoItems.Queries.GetAllTodoItems;
using FoodBlog.WebApi.Extensions;

namespace FoodBlog.WebApi.Features;

public static class TodoItemEndpoints
{
    public static void MapTodoItemEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("todoitems")
            .WithTags("TodoItems")
            .WithOpenApi();

        group
            .MapGet("/", (ISender sender, CancellationToken ct)
                => sender.Send(new GetAllTodoItemsQuery(), ct))
            .WithName("GetTodoItems")
            .ProducesGet<TodoItemDto[]>();

        // TODO: Investigate examples for swagger docs. i.e. better docs than:
        // myWeirdField: "string" vs myWeirdField: "this-silly-string"
        // (https://github.com/SSWConsulting/FoodBlog/issues/79)

        group
            .MapPost("/", (ISender sender, CreateTodoItemCommand command, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateTodoItem")
            .ProducesPost();
    }
}