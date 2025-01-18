using System.Data;
using System.Globalization;
using CashFlow.Communication.Request;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage(ResourcesErrorsMessages.TITLE_REQUIRED);
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(ResourcesErrorsMessages.AMOUT_REQUIRED) ;
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourcesErrorsMessages.DATE_REQUIRED);
        RuleFor(x => x.PaymentType).IsInEnum().WithMessage(ResourcesErrorsMessages.PAYMENTTYPE_REQUIRED);
    }
}