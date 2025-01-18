using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetExpenseById;
public interface IGetExpenseByIdUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
