using FluentValidation;

namespace CleanArchitecture.Master.UseCases.Services.Levels.Commands.CreateLevelCommand;

public class CreateLevelCommandValidator : AbstractValidator<CreateLevelCommand>
{
    public CreateLevelCommandValidator()
    {
        RuleFor(l => l.CourseId).NotEmpty().NotNull();
        RuleFor(l => l.Name).NotEmpty().NotNull().MaximumLength(255);
        RuleFor(l => l.IsActive).NotEmpty().NotNull();
    }
}