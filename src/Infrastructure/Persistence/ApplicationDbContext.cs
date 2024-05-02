using Microsoft.EntityFrameworkCore;
using FoodBlog.Application.Common.Interfaces;
using FoodBlog.Domain.TodoItems;
using FoodBlog.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace FoodBlog.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions options,
    EntitySaveChangesInterceptor saveChangesInterceptor,
    DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Order of the interceptors is important
        optionsBuilder.AddInterceptors(saveChangesInterceptor, dispatchDomainEventsInterceptor);
    }
}