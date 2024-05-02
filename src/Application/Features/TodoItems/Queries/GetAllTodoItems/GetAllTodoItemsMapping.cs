using FoodBlog.Domain.TodoItems;

namespace FoodBlog.Application.Features.TodoItems.Queries.GetAllTodoItems;

public class GetAllTodoItemsMapping : Profile
{
    public GetAllTodoItemsMapping()
    {
        CreateMap<TodoItem, TodoItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}