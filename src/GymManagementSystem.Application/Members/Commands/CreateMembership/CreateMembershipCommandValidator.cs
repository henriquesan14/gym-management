using FluentValidation;

namespace GymManagementSystem.Application.Members.Commands.CreateMembership;

public class CreateMembershipCommandValidator : AbstractValidator<CreateMembershipCommand>
{
    public CreateMembershipCommandValidator()
    {
        RuleFor(x => x.MemberId)
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(x => x.DurationInMonths)
            .GreaterThan(0)
            .WithMessage("O campo {PropertyName} tem que ser maior que 0")
            .LessThanOrEqualTo(24)
            .WithMessage("O campo {PropertyName} não pode ser maior que 24");
    }
}
