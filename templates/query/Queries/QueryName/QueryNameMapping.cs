using FoodBlog.Domain.Entities;

namespace FoodBlog.Application.Features.EntityNames.Queries.QueryName;

public class QueryNameMapping : Profile
{
    public QueryNameMapping()
    {
        CreateMap<EntityName, EntityNameDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
    }
}
