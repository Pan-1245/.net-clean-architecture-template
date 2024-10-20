using CleanArchitecture.Shared.Dto;

namespace CleanArchitecture.Master.Dto;

public record LevelDto : AuditableEntityDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid CourseId { get; set; }
    public required string CourseName { get; set; }
    public bool IsActive { get; set; }
}