using CleanArchitecture.Shared.Domain.Enum;

namespace CleanArchitecture.Master.Dto.SearchFilter;

public record SearchSubjectCriteriaDto
{
    public Guid? CourseId { get; set; }
    public string? Name { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public SortingOrder? OrderBy { get; set; } = SortingOrder.ASC;
}