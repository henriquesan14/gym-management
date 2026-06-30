using FluentValidation;

namespace GymManagementSystem.Application.Auth.Commands.GenerateAccessToken;

public sealed class GenerateAccessTokenCommandValidator : AbstractValidator<GenerateAccessTokenCommand>
{
    public GenerateAccessTokenCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(c => c.Password).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");
    }
}
