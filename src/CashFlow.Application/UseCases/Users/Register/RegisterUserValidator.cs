using CashFlow.Communication.Request;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourcesErrorsMessages.NAME_EMPTY);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourcesErrorsMessages.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourcesErrorsMessages.EMAIL_INVALID);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}
