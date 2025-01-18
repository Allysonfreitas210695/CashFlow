using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Exception.Exceptions;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        return new ResponseRegisteredExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var listErrors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(listErrors);
        }
    }
}