﻿using Microsoft.EntityFrameworkCore;
using FoodBlog.Application.Common.Interfaces;
using FoodBlog.Domain.TodoItems;

namespace FoodBlog.Application.Features.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateTodoItemCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle).WithMessage("'{PropertyName}' must be unique");
    }

    // TODO DM: Consider pushing this business validation to the Domain
    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        var spec = new TodoItemByTitleSpec(title);

        var exists = await _dbContext.TodoItems
            .WithSpecification(spec)
            .AnyAsync(cancellationToken: cancellationToken);

        return !exists;
    }
}