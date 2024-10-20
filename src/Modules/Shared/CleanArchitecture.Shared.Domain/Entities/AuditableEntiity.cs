namespace CleanArchitecture.Shared.Domain.Entities;

public class AuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string UpdatedBy { get; set; }
}