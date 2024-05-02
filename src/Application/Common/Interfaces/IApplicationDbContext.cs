using Microsoft.EntityFrameworkCore;
using FoodBlog.Domain.TodoItems;

namespace FoodBlog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoItem> TodoItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}