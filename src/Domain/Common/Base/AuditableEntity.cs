﻿using FoodBlog.Domain.Common.Interfaces;

namespace FoodBlog.Domain.Common.Base;

public abstract class AuditableEntity : IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
