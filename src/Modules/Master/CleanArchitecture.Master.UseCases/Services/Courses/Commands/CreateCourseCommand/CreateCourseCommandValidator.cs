using FluentValidation;

namespace CleanArchitecture.Master.UseCases.Services.Courses.Commands.CreateCourseCommand;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().NotNull().MaximumLength(255);
        RuleFor(c => c.IsActive).NotEmpty().NotNull();
    }
}