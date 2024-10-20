namespace CleanArchitecture.Shared.Dto;

public record AuditableEntityDto
{
    public DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string UpdatedBy { get; set; }
}