using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public interface IRegisterExpenseUseCase
{
    Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request);
}
