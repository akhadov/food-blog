using FoodBlog.Application;
using FoodBlog.Infrastructure;
using FoodBlog.Infrastructure.Persistence;
using FoodBlog.WebApi;
using FoodBlog.WebApi.Features;
using FoodBlog.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddExceptionHandler<KnownExceptionsHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Initialise and seed database
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await initializer.InitializeAsync();
    await initializer.SeedAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHealthChecks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseRouting();

app.UseDefaultExceptionHandler();
app.MapTodoItemEndpoints();

app.Run();

public partial class Program { }
