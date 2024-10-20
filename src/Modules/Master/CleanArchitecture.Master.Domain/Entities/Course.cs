using CleanArchitecture.Shared.Domain.Entities;

namespace CleanArchitecture.Master.Domain.Entities;

public class Course : AuditableEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}