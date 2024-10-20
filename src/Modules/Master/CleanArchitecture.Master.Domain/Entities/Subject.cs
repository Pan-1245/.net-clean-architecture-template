using CleanArchitecture.Shared.Domain.Entities;

namespace CleanArchitecture.Master.Domain.Entities;

public class Subject : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}