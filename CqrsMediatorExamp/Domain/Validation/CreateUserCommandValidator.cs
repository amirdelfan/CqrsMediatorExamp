using CqrsMediatorExamp.Domain.Commands.Users;
using FluentValidation;

namespace CqrsMediatorExamp.Domain.Validation
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(3);
        }
    }
}
