using FluentValidation;

namespace GymManagementSystem.Application.Members.Commands.CreateMember;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(c => c.FullName).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .MaximumLength(200).WithMessage("O campo {PropertyName} não pode ter mais de 200 caracteres");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .EmailAddress().WithMessage("O campo {PropertyName} tem que ser um email válido")
            .MaximumLength(256).WithMessage("O campo {PropertyName} não pode ter mais de 256 caracteres");
    }
}
