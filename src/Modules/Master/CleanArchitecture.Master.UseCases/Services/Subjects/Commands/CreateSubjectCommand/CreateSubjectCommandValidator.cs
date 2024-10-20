using FluentValidation;

namespace CleanArchitecture.Master.UseCases.Services.Subjects.Commands.CreateSubjectCommand;

public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(s => s.CourseId).NotEmpty().NotNull();
        RuleFor(s => s.Name).NotEmpty().NotNull().MaximumLength(255);
        RuleFor(s => s.IsActive).NotEmpty().NotNull();
    }
}