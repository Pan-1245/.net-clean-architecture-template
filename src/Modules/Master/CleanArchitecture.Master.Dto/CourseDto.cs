using CleanArchitecture.Shared.Dto;

namespace CleanArchitecture.Master.Dto;

public record CourseDto : AuditableEntityDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public bool IsActive { get; set; }
}